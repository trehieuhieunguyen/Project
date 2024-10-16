using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class Hieuss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "messageId",
                table: "AuctionWinners",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuctionWinners_messageId",
                table: "AuctionWinners",
                column: "messageId");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionWinners_Messages_messageId",
                table: "AuctionWinners",
                column: "messageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionWinners_Messages_messageId",
                table: "AuctionWinners");

            migrationBuilder.DropIndex(
                name: "IX_AuctionWinners_messageId",
                table: "AuctionWinners");

            migrationBuilder.DropColumn(
                name: "messageId",
                table: "AuctionWinners");
        }
    }
}
