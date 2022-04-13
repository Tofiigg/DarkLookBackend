using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class BasketWatchedUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketWatches_AspNetUsers_AppUserId1",
                table: "BasketWatches");

            migrationBuilder.DropIndex(
                name: "IX_BasketWatches_AppUserId1",
                table: "BasketWatches");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "BasketWatches");

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "BasketWatches",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_BasketWatches_AppUserId",
                table: "BasketWatches",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketWatches_AspNetUsers_AppUserId",
                table: "BasketWatches",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketWatches_AspNetUsers_AppUserId",
                table: "BasketWatches");

            migrationBuilder.DropIndex(
                name: "IX_BasketWatches_AppUserId",
                table: "BasketWatches");

            migrationBuilder.AlterColumn<int>(
                name: "AppUserId",
                table: "BasketWatches",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AppUserId1",
                table: "BasketWatches",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketWatches_AppUserId1",
                table: "BasketWatches",
                column: "AppUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketWatches_AspNetUsers_AppUserId1",
                table: "BasketWatches",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
