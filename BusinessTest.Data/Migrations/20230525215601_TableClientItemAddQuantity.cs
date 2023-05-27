using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessTest.Data.Migrations
{
    public partial class TableClientItemAddQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "ClientItem",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "ClientItem");
        }
    }
}
