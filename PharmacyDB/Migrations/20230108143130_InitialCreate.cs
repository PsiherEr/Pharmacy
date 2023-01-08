using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PharmacyDB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Phone = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(name: "Full Name", type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPhone", x => x.Phone);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(name: "Full Name", type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Position = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    SellBy = table.Column<DateTime>(name: "Sell By", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pharmacies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false),
                    DirectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PharmacyId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarehouseId", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicinesInReceipts",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinesInReceipts", x => new { x.ReceiptId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_MedicinesInReceipts_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicinesInReceipts_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReceiptsAndClients",
                columns: table => new
                {
                    ReceiptId = table.Column<int>(type: "int", nullable: false),
                    ClientPhone = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceiptsAndClients", x => new { x.ReceiptId, x.ClientPhone });
                    table.ForeignKey(
                        name: "FK_ReceiptsAndClients_Clients_ClientPhone",
                        column: x => x.ClientPhone,
                        principalTable: "Clients",
                        principalColumn: "Phone",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ReceiptsAndClients_Receipts_ReceiptId",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SupplierId = table.Column<int>(type: "int", nullable: false),
                    ManagerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderId", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Employees_ManagerId",
                        column: x => x.ManagerId,
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicinesInWarehouses",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    WarehouseId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinesInWarehouses", x => new { x.WarehouseId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_MedicinesInWarehouses_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicinesInWarehouses_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicinesInOrders",
                columns: table => new
                {
                    MedicineId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinesInOrders", x => new { x.OrderId, x.MedicineId });
                    table.ForeignKey(
                        name: "FK_MedicinesInOrders_Medicines_MedicineId",
                        column: x => x.MedicineId,
                        principalTable: "Medicines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicinesInOrders_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Phone", "Full Name" },
                values: new object[,]
                {
                    { 672463891, "Vasily Vasilyev" },
                    { 682214212, "Ivan Ivanov" },
                    { 982154521, "Kondraty Kondratiev" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "Email", "Full Name", "Phone", "Position" },
                values: new object[,]
                {
                    { 1, "nafanya.k@gmail.com", "Nafanail Karov", 980098980, "Manager" },
                    { 2, "karpova.ribba@gmail.com", "Kseniya Karpova", 982158289, "Cashier" },
                    { 3, "baba.valya@gmail.com", "Valentina Starodubceva", 678921952, "Director" }
                });

            migrationBuilder.InsertData(
                table: "Medicines",
                columns: new[] { "Id", "Name", "Price", "Sell By" },
                values: new object[,]
                {
                    { 1, "Paracetamol", 250.50m, new DateTime(2024, 1, 3, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 2, "Antigrippin", 699.99m, new DateTime(2023, 5, 8, 0, 0, 0, 0, DateTimeKind.Local) },
                    { 3, "Papazin", 360.00m, new DateTime(2023, 7, 7, 0, 0, 0, 0, DateTimeKind.Local) }
                });

            migrationBuilder.InsertData(
                table: "Pharmacies",
                columns: new[] { "Id", "Address", "DirectorId", "Phone" },
                values: new object[] { 1, "Pushkina 9", 3, 12521215 });

            migrationBuilder.InsertData(
                table: "Receipts",
                columns: new[] { "Id", "CreationDate", "Price" },
                values: new object[,]
                {
                    { 1, new DateTime(2023, 1, 7, 0, 0, 0, 0, DateTimeKind.Local), 1221.00m },
                    { 2, new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), 699.99m },
                    { 3, new DateTime(2023, 1, 8, 0, 0, 0, 0, DateTimeKind.Local), 360.00m }
                });

            migrationBuilder.InsertData(
                table: "Suppliers",
                columns: new[] { "Id", "Address", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Bolivia", "Supplier of Medicine OFFICIAL", 215128991 },
                    { 2, "Moldova", "HorseMedicalSpecial", 562123251 }
                });

            migrationBuilder.InsertData(
                table: "Warehouses",
                columns: new[] { "Id", "Address", "Phone" },
                values: new object[,]
                {
                    { 1, "Nowhere st. 10", 67212511 },
                    { 2, "Somewhere st. 24", 67512215 }
                });

            migrationBuilder.InsertData(
                table: "MedicinesInReceipts",
                columns: new[] { "MedicineId", "ReceiptId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 501.00m, 2 },
                    { 3, 1, 720.00m, 2 },
                    { 2, 2, 699.99m, 1 },
                    { 3, 3, 360.00m, 1 }
                });

            migrationBuilder.InsertData(
                table: "MedicinesInWarehouses",
                columns: new[] { "MedicineId", "WarehouseId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 40 },
                    { 2, 1, 29 },
                    { 1, 2, 8 },
                    { 3, 2, 42 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ManagerId", "OrderDate", "Price", "SupplierId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 12, 19, 0, 0, 0, 0, DateTimeKind.Local), 7525.00m, 1 },
                    { 2, 1, new DateTime(2022, 12, 14, 0, 0, 0, 0, DateTimeKind.Local), 16499.70m, 1 },
                    { 3, 1, new DateTime(2022, 12, 29, 0, 0, 0, 0, DateTimeKind.Local), 13950.00m, 2 }
                });

            migrationBuilder.InsertData(
                table: "ReceiptsAndClients",
                columns: new[] { "ClientPhone", "ReceiptId" },
                values: new object[,]
                {
                    { 682214212, 1 },
                    { 672463891, 2 },
                    { 682214212, 3 }
                });

            migrationBuilder.InsertData(
                table: "MedicinesInOrders",
                columns: new[] { "MedicineId", "OrderId", "Price", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 7525.00m, 50 },
                    { 2, 1, 16499.70m, 30 },
                    { 3, 2, 13950.00m, 45 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicinesInOrders_MedicineId",
                table: "MedicinesInOrders",
                column: "MedicineId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicinesInReceipts_MedicineId",
                table: "MedicinesInReceipts",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicinesInWarehouses_MedicineId",
                table: "MedicinesInWarehouses",
                column: "MedicineId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ManagerId",
                table: "Orders",
                column: "ManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_SupplierId",
                table: "Orders",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptsAndClients_ClientPhone",
                table: "ReceiptsAndClients",
                column: "ClientPhone");

            migrationBuilder.CreateIndex(
                name: "IX_ReceiptsAndClients_ReceiptId",
                table: "ReceiptsAndClients",
                column: "ReceiptId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicinesInOrders");

            migrationBuilder.DropTable(
                name: "MedicinesInReceipts");

            migrationBuilder.DropTable(
                name: "MedicinesInWarehouses");

            migrationBuilder.DropTable(
                name: "Pharmacies");

            migrationBuilder.DropTable(
                name: "ReceiptsAndClients");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
