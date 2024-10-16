using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class AddMoneyBack : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "MoneyBack",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MoneyBack",
                table: "Messages");
        }
    }
}
