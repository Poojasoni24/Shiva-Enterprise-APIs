using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shiva_Enterprise_APIs.Migrations
{
    /// <inheritdoc />
    public partial class ShivaMigration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vendor_bank",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_vendor_tax",
                table: "Vendor");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaxId",
                table: "Vendor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BankId",
                table: "Vendor",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_vendor_bank",
                table: "Vendor",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "BankId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vendor_tax",
                table: "Vendor",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "TaxId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_vendor_bank",
                table: "Vendor");

            migrationBuilder.DropForeignKey(
                name: "FK_vendor_tax",
                table: "Vendor");

            migrationBuilder.AlterColumn<Guid>(
                name: "TaxId",
                table: "Vendor",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BankId",
                table: "Vendor",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_vendor_bank",
                table: "Vendor",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_vendor_tax",
                table: "Vendor",
                column: "TaxId",
                principalTable: "Tax",
                principalColumn: "TaxId");
        }
    }
}
