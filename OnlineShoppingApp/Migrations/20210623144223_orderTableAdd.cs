using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineShoppingApp.Migrations
{
    public partial class orderTableAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OrderProducts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductId = table.Column<string>(nullable: true),
                    ProductCode = table.Column<string>(nullable: true),
                    ProductImage = table.Column<string>(nullable: true),
                    ProductPrice = table.Column<string>(nullable: true),
                    TotalAmount = table.Column<string>(nullable: true),
                    ProductQty = table.Column<string>(nullable: true),
                    OrderDateTime = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    UserPhoneNo = table.Column<string>(nullable: true),
                    UserAdress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProducts", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderProducts");
        }
    }
}
