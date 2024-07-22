using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shiva_Enterprise_APIs.Migrations
{
    /// <inheritdoc />
    public partial class shivamigration22 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CityId",
                table: "Customer",
                newName: "cityId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_CityId",
                table: "Customer",
                newName: "IX_Customer_cityId");

            migrationBuilder.CreateTable(
                name: "SalesReturn",
                columns: table => new
                {
                    SalesReturnID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SalesOrderID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReasonForReturn = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ReturnedQuantity = table.Column<int>(type: "int", nullable: false),
                    RestockingFee = table.Column<decimal>(type: "decimal(10,2)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SalesReturn", x => x.SalesReturnID);
                    table.ForeignKey(
                        name: "FK_SalesReturn_SalesOrder_SalesOrderID",
                        column: x => x.SalesOrderID,
                        principalTable: "SalesOrder",
                        principalColumn: "SalesOrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SalesReturn_SalesOrderID",
                table: "SalesReturn",
                column: "SalesOrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SalesReturn");

            migrationBuilder.RenameColumn(
                name: "cityId",
                table: "Customer",
                newName: "CityId");

            migrationBuilder.RenameIndex(
                name: "IX_Customer_cityId",
                table: "Customer",
                newName: "IX_Customer_CityId");
        }
    }
}
