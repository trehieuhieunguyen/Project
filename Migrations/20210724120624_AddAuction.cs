using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class AddAuctionWinner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price_Step",
                table: "Auctions");

            migrationBuilder.CreateTable(
                name: "AuctionWinners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<int>(type: "int", nullable: false),
                    auctionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ApplicationWinnerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    applicationUserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionWinners", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionWinners_AspNetUsers_applicationUserId",
                        column: x => x.applicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AuctionWinners_Auctions_auctionId",
                        column: x => x.auctionId,
                        principalTable: "Auctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionWinners_applicationUserId",
                table: "AuctionWinners",
                column: "applicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionWinners_auctionId",
                table: "AuctionWinners",
                column: "auctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionWinners");

            migrationBuilder.AddColumn<double>(
                name: "Price_Step",
                table: "Auctions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
