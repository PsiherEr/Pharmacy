using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacyDB.Migrations
{
    /// <inheritdoc />
    public partial class AddManagerIdInWarehouse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ManagerId",
                table: "Warehouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: 1,
                column: "ManagerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Warehouses",
                keyColumn: "Id",
                keyValue: 2,
                column: "ManagerId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Warehouses_ManagerId",
                table: "Warehouses",
                column: "ManagerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Warehouses_Employees_ManagerId",
                table: "Warehouses",
                column: "ManagerId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warehouses_Employees_ManagerId",
                table: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_Warehouses_ManagerId",
                table: "Warehouses");

            migrationBuilder.DropColumn(
                name: "ManagerId",
                table: "Warehouses");
        }
    }
}
