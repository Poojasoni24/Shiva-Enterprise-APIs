using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shiva_Enterprise_APIs.Migrations
{
    /// <inheritdoc />
    public partial class ShivaMigration17 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    VendorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    VendorCode = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    VendorName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    VendorType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    VendorAddress = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    Phoneno = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TaxId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ContractStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ContractEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Remark = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__vendor__B94AD674532DF6E8", x => x.VendorId);
                    table.ForeignKey(
                        name: "FK_vendor_bank",
                        column: x => x.BankId,
                        principalTable: "Bank",
                        principalColumn: "BankId");
                    table.ForeignKey(
                        name: "FK_vendor_tax",
                        column: x => x.TaxId,
                        principalTable: "Tax",
                        principalColumn: "TaxId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_VendorID",
                table: "PurchaseOrders",
                column: "VendorID");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_BankId",
                table: "Vendor",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_TaxId",
                table: "Vendor",
                column: "TaxId");

            migrationBuilder.AddForeignKey(
                name: "FK_purchaseorder_vendor",
                table: "PurchaseOrders",
                column: "VendorID",
                principalTable: "Vendor",
                principalColumn: "VendorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_purchaseorder_vendor",
                table: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseOrders_VendorID",
                table: "PurchaseOrders");
        }
    }
}
