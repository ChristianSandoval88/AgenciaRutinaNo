using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace POS.Data.Migrations
{
    public partial class ProductChangeProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceBuy",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Product",
                newName: "ShortDescription");

            migrationBuilder.AddColumn<bool>(
                name: "IsCustomizable",
                table: "Product",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LongDescription",
                table: "Product",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCustomizable",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "LongDescription",
                table: "Product");

            migrationBuilder.RenameColumn(
                name: "ShortDescription",
                table: "Product",
                newName: "Code");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceBuy",
                table: "Product",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
