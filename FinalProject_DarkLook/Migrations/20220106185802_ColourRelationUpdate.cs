using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class ColourRelationUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Colour_WatchCards_WatchCardId",
                table: "Colour");

            migrationBuilder.DropIndex(
                name: "IX_Colour_WatchCardId",
                table: "Colour");

            migrationBuilder.DropColumn(
                name: "WatchCardId",
                table: "Colour");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Colour",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Colour",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.AddColumn<int>(
                name: "WatchCardId",
                table: "Colour",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Colour_WatchCardId",
                table: "Colour",
                column: "WatchCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_Colour_WatchCards_WatchCardId",
                table: "Colour",
                column: "WatchCardId",
                principalTable: "WatchCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
