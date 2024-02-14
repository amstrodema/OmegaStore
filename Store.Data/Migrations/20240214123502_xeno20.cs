using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class xeno20 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Addr1",
                table: "ShippingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Addr2",
                table: "ShippingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "ShippingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "ShippingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "ShippingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "ShippingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "ShippingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "ShippingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "ShippingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "ShippingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Addr1",
                table: "BillingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Addr2",
                table: "BillingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "BillingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "BillingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "BillingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FName",
                table: "BillingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LName",
                table: "BillingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "BillingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Tel",
                table: "BillingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "BillingDetails",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Addr1",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "Addr2",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "City",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "FName",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "State",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "ShippingDetails");

            migrationBuilder.DropColumn(
                name: "Addr1",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "Addr2",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "City",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "FName",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "LName",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "State",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "Tel",
                table: "BillingDetails");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "BillingDetails");
        }
    }
}
