using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shiva_Enterprise_APIs.Migrations
{
    /// <inheritdoc />
    public partial class ShivaMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    BankCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    BankName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    BankDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    BankStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__bank__B94AD674532DF6E8", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "ModeofPayment",
                columns: table => new
                {
                    MODId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    MODCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    MODName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    MODDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    MODStatus = table.Column<bool>(type: "bit", nullable: false),
                    MODType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    MODAccount = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__mod__B94AD674532DF6E8", x => x.MODId);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                columns: table => new
                {
                    TaxId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TaxCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    TaxName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TaxDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    TaxStatus = table.Column<bool>(type: "bit", nullable: false),
                    TaxType = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    TaxRate = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tax__B94AD674532DF6E8", x => x.TaxId);
                });

            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    TransportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    TransportCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    TransportName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    TransportDescription = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    TransportStatus = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "(getdate())"),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__transport__B94AD674532DF6E8", x => x.TransportId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropTable(
                name: "ModeofPayment");

            migrationBuilder.DropTable(
                name: "Tax");

            migrationBuilder.DropTable(
                name: "Transport");
        }
    }
}
