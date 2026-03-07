using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookShopping_MVC.Migrations
{
    /// <inheritdoc />
    public partial class ththth : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GenerId",
                table: "Book");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GenerId",
                table: "Book",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
