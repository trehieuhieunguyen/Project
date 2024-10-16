using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class AddNewAuction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price_End",
                table: "Auctions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_End",
                table: "Auctions");
        }
    }
}
