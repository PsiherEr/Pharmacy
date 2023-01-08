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



namespace PharmacyDB
{
    internal class Program
    {
        static AutoResetEvent waitHandler = new AutoResetEvent(true);
        static void Main(string[] args)
        {
            DefaultDatabase();
            Console.WriteLine("\nRead: ");
            Read_LINQ_Query_Syntax();
            Create();
            Console.WriteLine("\nCreate: ");
            Read_LINQ_Query_Syntax();
            Update();
            Console.WriteLine("\nUpdate: ");
            Read_LINQ_Query_Syntax();
            Delete();
            Console.WriteLine("\nDelete: ");
            Read_LINQ_Query_Syntax();
        }

        public static void DefaultDatabase()
        {
            ApplicationDBContext context = new ApplicationDBContext();
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
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
