using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shiva_Enterprise_APIs.Migrations
{
    /// <inheritdoc />
    public partial class ShivaMigration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "salesmanAgent");

            migrationBuilder.RenameColumn(
                name: "TransportStatus",
                table: "Transport",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "TaxStatus",
                table: "Tax",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Country_ID",
                table: "state",
                newName: "Country_Id");

            migrationBuilder.RenameColumn(
                name: "State_ID",
                table: "state",
                newName: "State_Id");

            migrationBuilder.RenameColumn(
                name: "StateCode",
                table: "state",
                newName: "State_Code");

            migrationBuilder.RenameIndex(
                name: "IX_state_Country_ID",
                table: "state",
                newName: "IX_state_Country_Id");

            migrationBuilder.RenameColumn(
                name: "ProductTypeStatus",
                table: "ProductType",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "ProductGroupStatus",
                table: "ProductGroup",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "ProductCategoryStatus",
                table: "ProductCategory",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "ProductStatus",
                table: "Product",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "MODStatus",
                table: "ModeofPayment",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "Country_ID",
                table: "Country",
                newName: "Country_Id");

            migrationBuilder.RenameColumn(
                name: "State_id",
                table: "City",
                newName: "State_Id");

            migrationBuilder.RenameColumn(
                name: "City_ID",
                table: "City",
                newName: "City_Id");

            migrationBuilder.RenameColumn(
                name: "CityName",
                table: "City",
                newName: "City_Name");

            migrationBuilder.RenameColumn(
                name: "CityCode",
                table: "City",
                newName: "City_Code");

            migrationBuilder.RenameIndex(
                name: "IX_City_State_id",
                table: "City",
                newName: "IX_City_State_Id");

            migrationBuilder.RenameColumn(
                name: "BankStatus",
                table: "Bank",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "AccountTypeStatus",
                table: "AccountType",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "AccountGroupStatus",
                table: "AccountGroup",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "AccountCategoryStatus",
                table: "AccountCategory",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "AccountStatus",
                table: "Account",
                newName: "IsActive");

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "state",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "salesmanAgent",
                type: "bit",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Country",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "City",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "state");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "salesmanAgent");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Country");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "City");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Transport",
                newName: "TransportStatus");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Tax",
                newName: "TaxStatus");

            migrationBuilder.RenameColumn(
                name: "Country_Id",
                table: "state",
                newName: "Country_ID");

            migrationBuilder.RenameColumn(
                name: "State_Id",
                table: "state",
                newName: "State_ID");

            migrationBuilder.RenameColumn(
                name: "State_Code",
                table: "state",
                newName: "StateCode");

            migrationBuilder.RenameIndex(
                name: "IX_state_Country_Id",
                table: "state",
                newName: "IX_state_Country_ID");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "ProductType",
                newName: "ProductTypeStatus");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "ProductGroup",
                newName: "ProductGroupStatus");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "ProductCategory",
                newName: "ProductCategoryStatus");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Product",
                newName: "ProductStatus");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "ModeofPayment",
                newName: "MODStatus");

            migrationBuilder.RenameColumn(
                name: "Country_Id",
                table: "Country",
                newName: "Country_ID");

            migrationBuilder.RenameColumn(
                name: "State_Id",
                table: "City",
                newName: "State_id");

            migrationBuilder.RenameColumn(
                name: "City_Id",
                table: "City",
                newName: "City_ID");

            migrationBuilder.RenameColumn(
                name: "City_Name",
                table: "City",
                newName: "CityName");

            migrationBuilder.RenameColumn(
                name: "City_Code",
                table: "City",
                newName: "CityCode");

            migrationBuilder.RenameIndex(
                name: "IX_City_State_Id",
                table: "City",
                newName: "IX_City_State_id");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Bank",
                newName: "BankStatus");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AccountType",
                newName: "AccountTypeStatus");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AccountGroup",
                newName: "AccountGroupStatus");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "AccountCategory",
                newName: "AccountCategoryStatus");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Account",
                newName: "AccountStatus");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "salesmanAgent",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: true);
        }
    }
}
