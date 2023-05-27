using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessTest.Data.Migrations
{
    public partial class RelationshipClientRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Client_RoleId",
                table: "Client",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Client_Role_RoleId",
                table: "Client",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Client_Role_RoleId",
                table: "Client");

            migrationBuilder.DropIndex(
                name: "IX_Client_RoleId",
                table: "Client");
        }
    }
}
