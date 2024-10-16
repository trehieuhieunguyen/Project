using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class EditAuction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auctions_AspNetUsers_ApplicationUserBuyId",
                table: "Auctions");

            migrationBuilder.DropIndex(
                name: "IX_Auctions_ApplicationUserBuyId",
                table: "Auctions");

            migrationBuilder.DropColumn(
                name: "ApplicationUserBuyId",
                table: "Auctions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserBuyId",
                table: "Auctions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Auctions_ApplicationUserBuyId",
                table: "Auctions",
                column: "ApplicationUserBuyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Auctions_AspNetUsers_ApplicationUserBuyId",
                table: "Auctions",
                column: "ApplicationUserBuyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
