using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class AddEditAuctionWinner1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "AuctionWinners");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationId",
                table: "AuctionWinners",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
