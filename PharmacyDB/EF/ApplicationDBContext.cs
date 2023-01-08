using Microsoft.CSharp.RuntimeBinder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PharmacyDB.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace PharmacyDB.EF
{
    internal class ApplicationDBContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Receipt> Receipts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ReceiptAndClient> ReceiptsAndClients { get; set; }
        public DbSet<MedicineInOrder> MedicinesInOrders { get; set; }
        public DbSet<MedicineInReceipt> MedicinesInReceipts { get; set; }
        public DbSet<MedicineInWarehouse> MedicinesInWarehouses { get; set; }


        public ApplicationDBContext() {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Client client1 = new Client 
            { 
                Phone = 0682214212, 
                FullName = "Ivan Ivanov" 
            };
            
            Client client2 = new Client 
            { 
                Phone = 0672463891, 
                FullName = "Vasily Vasilyev" 
            };
            
            Client client3 = new Client 
            { 
                Phone = 0982154521, 
                FullName = "Kondraty Kondratiev" 
            };

            Employee employee1 = new Employee
            {
                Id = 1,
                FullName = "Nafanail Karov",
                Phone = 0980098980,
                Email = "nafanya.k@gmail.com",
                Position = "Manager"
            };
            Employee employee2 = new Employee
            {
                Id = 2,
                FullName = "Kseniya Karpova",
                Phone = 0982158289,
                Email = "karpova.ribba@gmail.com",
                Position = "Cashier"
            };
            Employee employee3 = new Employee
            {
                Id = 3,
                FullName = "Valentina Starodubceva",
                Phone = 0678921952,
                Email = "baba.valya@gmail.com",
                Position = "Director"
            };

            Medicine medicine1 = new Medicine
            {
                Id = 1,
                Price = 250.50m,
                Name = "Paracetamol",
                SellBy = DateTime.Today.AddDays(360),
            };
            Medicine medicine2 = new Medicine
            {
                Id = 2,
                Price = 699.99m,
                Name = "Antigrippin",
                SellBy = DateTime.Today.AddDays(120),
            };
            Medicine medicine3 = new Medicine
            {
                Id = 3,
                Price = 360.00m,
                Name = "Papazin",
                SellBy = DateTime.Today.AddDays(180),
            };

            MedicineInOrder medicineInOrder1 = new MedicineInOrder
            {
                MedicineId = 1,
                OrderId = 1,
                Quantity = 50,
                Price = (medicine1.Price - 100) * 50
            };

            MedicineInOrder medicineInOrder2 = new MedicineInOrder
            {
                MedicineId = 2,
                OrderId = 1,
                Quantity = 30,
                Price = (medicine2.Price - 150) * 30
            };

            MedicineInOrder medicineInOrder3 = new MedicineInOrder
            {
                MedicineId = 3,
                OrderId = 2,
                Quantity = 45,
                Price = (medicine3.Price - 50) * 45
            };

            MedicineInReceipt medicineInReceipt1 = new MedicineInReceipt
            {
                MedicineId = 1,
                ReceiptId = 1,
                Quantity = 2,
                Price = medicine1.Price*2
            };

            MedicineInReceipt medicineInReceipt2 = new MedicineInReceipt
            {
                MedicineId = 2,
                ReceiptId = 2,
                Quantity = 1,
                Price = medicine2.Price
            };

            MedicineInReceipt medicineInReceipt3 = new MedicineInReceipt
            {
                MedicineId = 3,
                ReceiptId = 1,
                Quantity = 2,
                Price = medicine3.Price * 2 
            };

            MedicineInReceipt medicineInReceipt4 = new MedicineInReceipt
            {
                MedicineId = 3,
                ReceiptId = 3,
                Quantity = 1,
                Price = medicine3.Price
            };

            MedicineInWarehouse medicineInWarehouse1 = new MedicineInWarehouse
            {
                MedicineId = 1,
                WarehouseId = 1,
                Quantity = 40
            };
            
            MedicineInWarehouse medicineInWarehouse2 = new MedicineInWarehouse
            {
                MedicineId = 1,
                WarehouseId = 2,
                Quantity = 8
            };

            MedicineInWarehouse medicineInWarehouse3 = new MedicineInWarehouse
            {
                MedicineId = 2,
                WarehouseId = 1,
                Quantity = 29
            };
            MedicineInWarehouse medicineInWarehouse4 = new MedicineInWarehouse
            {
                MedicineId = 3,
                WarehouseId = 2,
                Quantity = 42
            };

            Order order1 = new Order
            {
                Id = 1,
                Price = medicineInOrder1.Price,
                OrderDate = DateTime.Today.AddDays(-20),
                SupplierId = 1,
                ManagerId = 1
            };
            
            Order order2 = new Order
            {
                Id = 2,
                Price = medicineInOrder2.Price,
                OrderDate = DateTime.Today.AddDays(-25),
                SupplierId = 1,
                ManagerId = 1
            };

            Order order3 = new Order
            {
                Id = 3,
                Price = medicineInOrder3.Price,
                OrderDate = DateTime.Today.AddDays(-10),
                SupplierId = 2,
                ManagerId = 1
            };

            Pharmacy pharmacy1 = new Pharmacy
            {
                Id = 1,
                Address = "Pushkina 9",
                Phone = 12521215,
                DirectorId = 3
            };

            Receipt receipt1 = new Receipt
            {
                Id = 1,
                Price = medicineInReceipt1.Price + medicineInReceipt3.Price,
                CreationDate = DateTime.Today.AddDays(-1)
            };
            
            Receipt receipt2 = new Receipt
            {
                Id = 2,
                Price = medicineInReceipt2.Price,
                CreationDate = DateTime.Today
            };

            Receipt receipt3 = new Receipt
            {
                Id = 3,
                Price = medicineInReceipt4.Price,
                CreationDate = DateTime.Today
            };

            ReceiptAndClient receiptAndClient1 = new ReceiptAndClient 
            {
                ReceiptId = 1,
                ClientPhone = client1.Phone
            };

            ReceiptAndClient receiptAndClient2 = new ReceiptAndClient
            {
                ReceiptId = 2,
                ClientPhone = client2.Phone
            };

            ReceiptAndClient receiptAndClient3 = new ReceiptAndClient
            {
                ReceiptId = 3,
                ClientPhone = client1.Phone
            };

            Supplier supplier1 = new Supplier
            {
                Id = 1,
                Name = "Supplier of Medicine OFFICIAL",
                Address = "Bolivia",
                Phone = 0215128991
            };

            Supplier supplier2 = new Supplier
            {
                Id = 2,
                Name = "HorseMedicalSpecial",
                Address = "Moldova",
                Phone = 0562123251
            };

            Warehouse warehouse1 = new Warehouse
            {
                Id = 1,
                Address = "Nowhere st. 10",
                Phone = 067212511,
                ManagerId = 1
            };

            Warehouse warehouse2 = new Warehouse
            {
                Id = 2,
                Address = "Somewhere st. 24",
                Phone = 067512215,
                ManagerId = 1
            };

            modelBuilder.Entity<Client>().HasData(client1, client2, client3);
            modelBuilder.Entity<Employee>().HasData(employee1, employee2, employee3);
            modelBuilder.Entity<Medicine>().HasData(medicine1, medicine2, medicine3);
            modelBuilder.Entity<MedicineInOrder>().HasData(medicineInOrder1, medicineInOrder2, medicineInOrder3);
            modelBuilder.Entity<MedicineInReceipt>().HasData(medicineInReceipt1, medicineInReceipt2, medicineInReceipt3, medicineInReceipt4);
            modelBuilder.Entity<MedicineInWarehouse>().HasData(medicineInWarehouse1, medicineInWarehouse2, medicineInWarehouse3, medicineInWarehouse4);
            modelBuilder.Entity<Order>().HasData(order1, order2, order3);
            modelBuilder.Entity<Pharmacy>().HasData(pharmacy1);
            modelBuilder.Entity<Receipt>().HasData(receipt1, receipt2, receipt3);
            modelBuilder.Entity<ReceiptAndClient>().HasData(receiptAndClient1, receiptAndClient2, receiptAndClient3);
            modelBuilder.Entity<Supplier>().HasData(supplier1, supplier2);
            modelBuilder.Entity<Warehouse>().HasData(warehouse1, warehouse2);

            modelBuilder
                .Entity<Client>()
                .HasKey(x => x.Phone)
                .HasName("PK_ClientPhone");
            
            modelBuilder
                .Entity<Client>()
                .Property(x => x.Phone)
                .ValueGeneratedNever();


            modelBuilder
                .Entity<Client>()
                .Property(x => x.FullName)
                .HasColumnName("Full Name")
                .HasMaxLength(128);

            modelBuilder
                .Entity<Employee>()
                .HasKey(x => x.Id)
                .HasName("PK_EmployeeId");


            modelBuilder
                .Entity<Employee>()
                .Property(x => x.FullName)
                .HasColumnName("Full Name")
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder
                .Entity<Employee>()
                .Property(x => x.Email)
                .HasMaxLength(32)
                .IsRequired();

            modelBuilder
                .Entity<Employee>()
                .Property(x => x.Position)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder
                .Entity<Medicine>()
                .HasKey(x => x.Id)
                .HasName("PK_MedicineId");

           modelBuilder
                .Entity<Medicine>()
                .HasCheckConstraint("Price", "Price >= 0");

            modelBuilder
                .Entity<Medicine>()
                .Property(x => x.Price)
                .HasDefaultValue(0);

            modelBuilder
                .Entity<Medicine>()
                .Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder
                .Entity<Medicine>()
                .Property(x => x.SellBy)
                .HasColumnName("Sell By");

            modelBuilder
                .Entity<MedicineInOrder>()
                .HasOne(x => x.Order)
                .WithMany(x => x.MedicinesInOrder)
                .HasForeignKey(x => x.OrderId)
                .IsRequired();

            modelBuilder
                .Entity<MedicineInOrder>()
                .HasOne(x => x.Medicine)
                .WithOne(x => x.MedicineInOrder)
                .HasForeignKey<MedicineInOrder>(x => x.MedicineId)
                .IsRequired();

            modelBuilder
                .Entity<MedicineInOrder>()
                .HasKey(x => new { x.OrderId, x.MedicineId });

            modelBuilder
                .Entity<MedicineInOrder>()
                .HasCheckConstraint("Price", "Price >= 0");

            modelBuilder
                .Entity<MedicineInOrder>()
                .Property(x => x.Price)
                .HasDefaultValue(0);

            modelBuilder
                .Entity<MedicineInReceipt>()
                .HasOne(x => x.Medicine)
                .WithMany(x => x.MedicineInReceipts)
                .HasForeignKey(x => x.MedicineId)
                .IsRequired();

            modelBuilder
                .Entity<MedicineInReceipt>()
                .HasOne(x => x.Receipt)
                .WithMany(x => x.MedicinesInReceipt)
                .HasForeignKey(x => x.ReceiptId)
                .IsRequired();

            modelBuilder
                .Entity<MedicineInReceipt>()
                .HasKey(x => new { x.ReceiptId, x.MedicineId });

            modelBuilder
                .Entity<MedicineInReceipt>()
                .HasCheckConstraint("Price", "Price >= 0");

            modelBuilder
                .Entity<MedicineInReceipt>()
                .Property(x => x.Price)
                .HasDefaultValue(0);

            modelBuilder
                .Entity<MedicineInWarehouse>()
                .HasOne(x => x.Medicine)
                .WithMany(x => x.MedicineInWarehouses)
                .HasForeignKey(x => x.MedicineId)
                .IsRequired();

            modelBuilder
                .Entity<MedicineInWarehouse>()
                .HasOne(x => x.Warehouse)
                .WithMany(x => x.MedicinesInWarehouse)
                .HasForeignKey(x => x.WarehouseId)
                .IsRequired();

            modelBuilder
                .Entity<MedicineInWarehouse>()
                .HasKey(x => new { x.WarehouseId, x.MedicineId });

            modelBuilder
                .Entity<Order>()
                .HasKey(x => x.Id)
                .HasName("PK_OrderId");

            modelBuilder
                .Entity<Order>()
                .HasCheckConstraint("Price", "Price >= 0");

            modelBuilder
                .Entity<Order>()
                .Property(x => x.Price)
                .HasDefaultValue(0);

            modelBuilder
                .Entity<Pharmacy>()
                .HasKey(x => x.Id)
                .HasName("PK_PharmacyId");

            modelBuilder
                .Entity<Pharmacy>()
                .Property(x => x.Address)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder
                .Entity<Receipt>()
                .HasKey(x => x.Id)
                .HasName("PK_ReceiptId");

            modelBuilder
                .Entity<Receipt>()
                .HasCheckConstraint("Price", "Price >= 0");

            modelBuilder
                .Entity<Receipt>()
                .Property(x => x.Price)
                .HasDefaultValue(0);

            modelBuilder
                .Entity<ReceiptAndClient>()
                .HasOne(x => x.Receipt)
                .WithOne(x => x.ReceiptAndClient)
                .HasForeignKey<ReceiptAndClient>(x => x.ReceiptId)
                .IsRequired();

            modelBuilder
                .Entity<ReceiptAndClient>()
                .HasOne(x => x.Client)
                .WithMany(x => x.ReceiptsAndClient)
                .HasForeignKey(x => x.ClientPhone)
                .IsRequired();

            modelBuilder
                .Entity<ReceiptAndClient>()
                .HasKey(x => new { x.ReceiptId, x.ClientPhone });

            modelBuilder
                .Entity<Supplier>()
                .HasKey(x => x.Id)
                .HasName("PK_SupplierId");

            modelBuilder
                .Entity<Supplier>()
                .Property(x => x.Name)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder
                .Entity<Supplier>()
                .Property(x => x.Address)
                .HasMaxLength(128);

            modelBuilder
                .Entity<Warehouse>()
                .HasKey(x => x.Id)
                .HasName("PK_WarehouseId");

            modelBuilder
                .Entity<Warehouse>()
                .Property(x => x.Address)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder
                .Entity<Warehouse>()
                .Property(x => x.ManagerId)
                .IsRequired();



        }
    }

}
