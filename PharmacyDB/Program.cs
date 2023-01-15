using PharmacyDB.EF;
using PharmacyDB.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Security.Cryptography;

namespace PharmacyDB
{
    internal class Program
    {
        static AutoResetEvent waitHandler = new AutoResetEvent(true);
        static void Main(string[] args)
        {
            DefaultDatabase();


            /*Console.WriteLine("\nRead: ");
            Read_LINQ_Query_Syntax();
            Create();
            Console.WriteLine("\nCreate: ");
            Read_LINQ_Query_Syntax();
            Update();
            Console.WriteLine("\nUpdate: ");
            Read_LINQ_Query_Syntax();
            Delete();
            Console.WriteLine("\nDelete: ");
            Read_LINQ_Query_Syntax();*/

            Console.WriteLine("\n------------------------Union-------------------------------");
            Union();
            Console.WriteLine("\n------------------------Except-------------------------------");
            Except();
            Console.WriteLine("\n------------------------Intersect-------------------------------");
            Intersect();
            Console.WriteLine("\n------------------------Join-------------------------------");
            Join();
            Console.WriteLine("\n------------------------GroupBy-------------------------------");
            GroupBy();
            Console.WriteLine("\n------------------------Distinct-------------------------------");
            Distinct();
            Console.WriteLine("\n------------------------Any-------------------------------");
            Any();
            Console.WriteLine("\n------------------------All-------------------------------");
            All();
            Console.WriteLine("\n------------------------Min-------------------------------");
            Min();
            Console.WriteLine("\n------------------------Max-------------------------------");
            Max();
            Console.WriteLine("\n------------------------Average-------------------------------");
            Average();
            Console.WriteLine("\n------------------------Sum-------------------------------");
            Sum();
            Console.WriteLine("\n------------------------Count-------------------------------");
            Count();
            Console.WriteLine("\n------------------------ExplicitLoading-------------------------------");
            ExplicitLoading();
            Console.WriteLine("\n------------------------LazyLoading-------------------------------");
            LazyLoading();
            Console.WriteLine("\n------------------------EagerLoading-------------------------------");
            EagerLoading();
            Console.WriteLine("\n------------------------AsNotTracking-------------------------------");
            AsNotTracking();
            Console.WriteLine("\n------------------------Procedure-------------------------------");
            Procedure();
            Console.WriteLine("\n------------------------Function-------------------------------");
            Function();
        }

        public static void DefaultDatabase()
        {
            ApplicationDBContext context = new ApplicationDBContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var createSql = @"
                create procedure [dbo].[GetMedicinesByPrice] as
                begin
                    select * from dbo.Medicines
                    order by Price desc
                end
            ";

            context.Database.ExecuteSqlRaw(createSql);

            createSql = @"
                create function [dbo].[SearchOrdersBySupplierId] (@id int)
                returns table
                as
                return
                    select * from dbo.Orders
                    where SupplierId = @id
            ";

            context.Database.ExecuteSqlRaw(createSql);
        }

        public static void Union()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var query = context.Clients.Select(x => x.FullName)
                .Union(context.Employees.Select(x => x.FullName));

            foreach (var item in query)
            {
                Console.WriteLine($"{item}");
            }
        }
        public static void Except()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var selector1 = context.Medicines.ToList();
            var selector2 = context.Medicines.Where(x => x.Price > 600).ToList();

            var query = selector1.Except(selector2);

            foreach (var item in query)
            {
                Console.WriteLine($"MedicineId: {item.Id}, Name: {item.Name}, Price: {item.Price}");
            }
        }

        public static void Intersect()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var query = context.Employees.Select(x => x.Id)
                .Intersect(context.Orders.Select(x => x.ManagerId));

            foreach (var item in query)
            {
                Console.WriteLine($"ManagerId: {item}");
            }
        }

        public static void Join()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var query = context.Orders
                .Join(context.Employees,
                order => order.ManagerId,
                employee => employee.Id,
                (order, employee) => new
                {
                    OrderId = order.Id,
                    ManagerId = employee.Id,
                    ManagerName = employee.FullName
                });

            foreach (var item in query)
            {
                Console.WriteLine($"OrderId: {item.OrderId} ManagerId: {item.ManagerId} ManagerName: {item.ManagerName}");
            }
        }

        public static void GroupBy()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var groups = context.Receipts
                .GroupBy(x => x.ReceiptAndClient.ClientPhone)
                .Select(m => new
                {
                    m.Key,
                    Count = m.Count()
                })
                .ToList();

            foreach (var group in groups)
            {
                Console.WriteLine($"+380{group.Key} - {group.Count}");
            }
        }

        public static void Distinct()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var groups = context.Employees
                .Select(m => m.Position)
                .Distinct()
                .ToList();

            foreach (var group in groups)
            {
                Console.WriteLine($"{group}");
            }
        }

        public static void Any()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            bool result = context.Employees.Any(u => u.Position == "Cashier");

            Console.WriteLine(result);
        }

        public static void All()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            bool result = context.Employees.All(u => u.Position == "Cashier");

            Console.WriteLine(result);
        }

        public static void Min()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var minPrice = context.Medicines.Min(u => u.Price);

            Console.WriteLine($"Min price = {minPrice}");
        }

        public static void Max()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var maxPrice = context.Medicines.Max(u => u.Price);

            Console.WriteLine($"Max price = {maxPrice}");
        }

        public static void Average()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var avgPrice = context.Medicines.Average(u => u.Price);

            Console.WriteLine($"Average price = {avgPrice}");
        }

        public static void Sum()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var sumPrice = context.Medicines.Sum(u => u.Price);

            Console.WriteLine($"Sum price = {sumPrice}");
        }

        public static void Count()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var meds = context.Medicines.Count();

            Console.WriteLine($"Count medicines = {meds}");
        }

        public static void ExplicitLoading()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var med = context.Medicines.First();

            context.MedicinesInReceipts.Where(x => x.MedicineId == med.Id).Load();
            context.Receipts.Where(x => x.MedicinesInReceipt.Any(x => x.MedicineId == med.Id)).Load();

            foreach (var item in med.MedicineInReceipts)
            {
                Console.WriteLine($"MedicineId: {item.MedicineId}, ReceiptId: {item.ReceiptId}");
            }
        }

        public static void LazyLoading()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var orders = context.Orders.ToList();

            foreach (var order in orders)
            {
                Console.WriteLine($"OrderId: {order.Id}, SupplierName: {order.Supplier.Name}");
            }
        }

            public static void EagerLoading()
        {
            ApplicationDBContext context = new ApplicationDBContext();
            
            var medicinesInWarehouses = context.MedicinesInWarehouses
                .Include(x => x.Medicine)
                .Include(x => x.Warehouse)
                .ToList();

            foreach (var item in medicinesInWarehouses)
            {
                Console.WriteLine($"Medicine: {item.Medicine.Name}, Warehouse: {item.Warehouse.Id}");
            }
        }

        public static void AsNotTracking()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var temp = context.Medicines.Where(x => x.Id == 2).Single();

            Console.WriteLine($"Price = {temp.Price}");

            temp.Price = 300.00m;

            context.SaveChanges();

            temp = context.Medicines.Where(x => x.Id == 2).AsNoTracking().Single();

            temp.Price = 325.00m;

            Console.WriteLine($"Price = {temp.Price}");

            context.SaveChanges();

            temp = context.Medicines.Where(x => x.Id == 2).AsNoTracking().Single();

            Console.WriteLine($"Price = {temp.Price}");
        }

        public static void Procedure()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var meds = context
                .Medicines
                .FromSqlRaw("EXECUTE dbo.GetMedicinesByPrice").ToList();

            Console.WriteLine("Medicines:");
            foreach (var item in meds)
            {
                Console.WriteLine("--------");
                Console.WriteLine(
                    $"Id: {item.Id}. " +
                    $"Name: {item.Name}. " +
                    $"Price: {item.Price}.");
            }
        }

        public static void Function()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var orders = context
                .Orders
                .FromSqlRaw("SELECT * FROM dbo.SearchOrdersBySupplierId(1)").ToList();
            
            Console.WriteLine("Found:");
            foreach (var item in orders)
            {
                Console.WriteLine("--------");
                Console.WriteLine(
                    $"Id: {item.Id}. " +
                    $"Price: {item.Price}.");
            }
        }

        public static void Read_LINQ_Query_Syntax()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var query = from client in context.Clients
                        join receiptAndClient in context.ReceiptsAndClients
                        on client.Phone equals receiptAndClient.ClientPhone
                        join receipt in context.Receipts
                        on receiptAndClient.ReceiptId equals receipt.Id
                        join medicineInReceipt in context.MedicinesInReceipts
                        on receipt.Id equals medicineInReceipt.ReceiptId
                        join medicine in context.Medicines
                        on medicineInReceipt.MedicineId equals medicine.Id
                        join medicineInOrder in context.MedicinesInOrders
                        on medicine.Id equals medicineInOrder.MedicineId
                        join order in context.Orders
                        on medicineInOrder.OrderId equals order.Id
                        select new
                        {
                            Client = client,
                            ReceiptAndClient = receiptAndClient,
                            Receipt = receipt,
                            MedicineInReceipt = medicineInReceipt,
                            Medicine = medicine,
                            MedicineInOrder = medicineInOrder,
                            Order = order
                        };

            foreach(var item in query)
            {
                Console.WriteLine($"\nClient: {item.Client.FullName} +380{item.Client.Phone}");
                Console.WriteLine($"Receipt Id:{item.Receipt.Id}  Creation: {item.Receipt.CreationDate.ToShortDateString()}  Price:{item.Receipt.Price}");
                Console.WriteLine($"Medicine: {item.Medicine.Name}  Price:{item.Medicine.Price}  Sell by {item.Medicine.SellBy.ToShortDateString()}");
                Console.WriteLine($"Order Id:{item.Order.Id}  Date: {item.Order.OrderDate.ToShortDateString()}");
            }
        }

        public static void Delete()
        {
            ApplicationDBContext context = new ApplicationDBContext();
            var temp = context.Receipts.Where(x => x.Id == 3).Single();
            context.Receipts.Remove(temp);
            context.SaveChanges();
        }
        public static void Create()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            Client client = new Client()
            {
                Phone = 12141212,
                FullName = "Aleksey Alekseev"
            };

            ReceiptAndClient receiptAndClient = new ReceiptAndClient();

            Receipt receipt = new Receipt()
            {
                Price = 100,
                CreationDate = DateTime.Now
            };

            MedicineInReceipt medicineInReceipt = new MedicineInReceipt()
            {
                Quantity = 2,
                Price = receipt.Price
            };

            Medicine medicine = new Medicine()
            {
                Name = "Aspirinus",
                Price = 50,
                SellBy = DateTime.Today.AddDays(120)
            };

            MedicineInOrder medicineInOrder = new MedicineInOrder()
            {
                Quantity = 4,
                Price = 120
            };

            Order order = new Order()
            {
                Price = medicineInOrder.Price,
                OrderDate = DateTime.Today.AddDays(-10),
            };

            MedicineInWarehouse medicineInWarehouse = new MedicineInWarehouse()
            {
                Quantity = 2
            };

            Warehouse warehouse = new Warehouse()
            {
                Address = "Kyiv, Khreshchatyk, 1",
                Phone = 1522152121,
                ManagerId = 1
            };

            receiptAndClient.Receipt = receipt;
            receiptAndClient.Client = client;

            medicineInReceipt.Medicine = medicine;
            medicineInReceipt.Receipt = receipt;

            medicineInOrder.Medicine = medicine;
            medicineInOrder.Order = order;

            order.Manager = context.Employees.First();
            order.Supplier = context.Suppliers.First();

            medicineInWarehouse.Medicine = medicine;
            medicineInWarehouse.Warehouse = warehouse;

            context.Add(client);
            context.Add(receiptAndClient);
            context.Add(receipt);
            context.Add(medicineInReceipt);
            context.Add(medicine);
            context.Add(medicineInOrder);
            context.Add(order);
            context.Add(medicineInWarehouse);
            context.Add(warehouse);

            context.SaveChanges();
        }

        public static void Update()
        {
            ApplicationDBContext context = new ApplicationDBContext();

            var temp = context.Clients.Where(x => x.Phone == 12141212).Single();

            temp.FullName = "Petro Alekseev";

            context.SaveChanges();
        }


    }
}
