using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class ffd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdmin",
                table: "Offers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsHomepage",
                table: "Offers",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAdmin",
                table: "Offers");

            migrationBuilder.DropColumn(
                name: "IsHomepage",
                table: "Offers");
        }
    }
}
