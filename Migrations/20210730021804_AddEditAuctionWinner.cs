using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class AddEditAuctionWinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MessageId",
                table: "AuctionWinners");

            migrationBuilder.RenameColumn(
                name: "ApplicationWinnerId",
                table: "AuctionWinners",
                newName: "ApplicationId");

            migrationBuilder.AddColumn<bool>(
                name: "DeliveryStatus",
                table: "AuctionWinners",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryStatus",
                table: "AuctionWinners");

            migrationBuilder.RenameColumn(
                name: "ApplicationId",
                table: "AuctionWinners",
                newName: "ApplicationWinnerId");

        }
    }
}
