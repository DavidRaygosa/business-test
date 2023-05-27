using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessTest.Data.Migrations
{
    public partial class RelationshipClientAuthentication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authentication_Client_ClientId",
                table: "Authentication");

            migrationBuilder.DropIndex(
                name: "IX_Authentication_ClientId",
                table: "Authentication");

            migrationBuilder.CreateIndex(
                name: "IX_Authentication_ClientId",
                table: "Authentication",
                column: "ClientId",
                unique: true,
                filter: "[ClientId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Authentication_Client_ClientId",
                table: "Authentication",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authentication_Client_ClientId",
                table: "Authentication");

            migrationBuilder.DropIndex(
                name: "IX_Authentication_ClientId",
                table: "Authentication");

            migrationBuilder.CreateIndex(
                name: "IX_Authentication_ClientId",
                table: "Authentication",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authentication_Client_ClientId",
                table: "Authentication",
                column: "ClientId",
                principalTable: "Client",
                principalColumn: "Id");
        }
    }
}
