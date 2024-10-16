using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class uuu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionWinners_Messages_messageId",
                table: "AuctionWinners");

            migrationBuilder.RenameColumn(
                name: "messageId",
                table: "AuctionWinners",
                newName: "MessageId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionWinners_messageId",
                table: "AuctionWinners",
                newName: "IX_AuctionWinners_MessageId");

            migrationBuilder.AlterColumn<int>(
                name: "MessageId",
                table: "AuctionWinners",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionWinners_Messages_MessageId",
                table: "AuctionWinners",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuctionWinners_Messages_MessageId",
                table: "AuctionWinners");

            migrationBuilder.RenameColumn(
                name: "MessageId",
                table: "AuctionWinners",
                newName: "messageId");

            migrationBuilder.RenameIndex(
                name: "IX_AuctionWinners_MessageId",
                table: "AuctionWinners",
                newName: "IX_AuctionWinners_messageId");

            migrationBuilder.AlterColumn<int>(
                name: "messageId",
                table: "AuctionWinners",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AuctionWinners_Messages_messageId",
                table: "AuctionWinners",
                column: "messageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
