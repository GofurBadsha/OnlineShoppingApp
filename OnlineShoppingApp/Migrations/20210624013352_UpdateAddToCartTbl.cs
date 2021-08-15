using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShoppingApp.Migrations
{
    public partial class UpdateAddToCartTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductImage",
                table: "AddToCarts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductPrice",
                table: "AddToCarts",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ProductTitle",
                table: "AddToCarts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductImage",
                table: "AddToCarts");

            migrationBuilder.DropColumn(
                name: "ProductPrice",
                table: "AddToCarts");

            migrationBuilder.DropColumn(
                name: "ProductTitle",
                table: "AddToCarts");
        }
    }
}
