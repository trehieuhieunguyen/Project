using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class EditAuctionVer1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Product_StatusBuy",
                table: "Auctions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Product_StatusBuy",
                table: "Auctions");
        }
    }
}
