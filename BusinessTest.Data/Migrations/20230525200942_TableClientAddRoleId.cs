using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessTest.Data.Migrations
{
    public partial class TableClientAddRoleId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "RoleId",
                table: "Client",
                type: "bigint",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "Client");
        }
    }
}
