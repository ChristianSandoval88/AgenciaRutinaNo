using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Data.Migrations
{
    public partial class AddPricesFieldsOnProductsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceSell2",
                table: "Product",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceSell2Desc",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceSell3",
                table: "Product",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceSell3Desc",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceSell4",
                table: "Product",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceSell4Desc",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PriceSellDesc",
                table: "Product",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceSell2",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PriceSell2Desc",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PriceSell3",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PriceSell3Desc",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PriceSell4",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PriceSell4Desc",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "PriceSellDesc",
                table: "Product");
        }
    }
}
