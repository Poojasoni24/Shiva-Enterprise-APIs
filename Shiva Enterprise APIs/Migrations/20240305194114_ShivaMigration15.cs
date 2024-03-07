﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shiva_Enterprise_APIs.Migrations
{
    /// <inheritdoc />
    public partial class ShivaMigration15 : Migration
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

            migrationBuilder.DropIndex(
                name: "IX_Vendor_BankId",
                table: "Vendor");

            migrationBuilder.DropIndex(
                name: "IX_Vendor_TaxId",
                table: "Vendor");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Vendor_BankId",
                table: "Vendor",
                column: "BankId");

            migrationBuilder.CreateIndex(
                name: "IX_Vendor_TaxId",
                table: "Vendor",
                column: "TaxId");

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
