using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class UpdateWinnerPriceEnd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "PriceEnd",
                table: "AuctionWinners",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceEnd",
                table: "AuctionWinners");
        }
    }
}
