using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.Data.Migrations
{
    /// <inheritdoc />
    public partial class xeno2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Reviews",
                table: "Items",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reviews",
                table: "Items");
        }
    }
}
