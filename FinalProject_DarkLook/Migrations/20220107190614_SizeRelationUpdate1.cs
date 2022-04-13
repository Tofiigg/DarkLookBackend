using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class SizeRelationUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchColours_Sizes_SizeId",
                table: "WatchColours");

            migrationBuilder.DropIndex(
                name: "IX_WatchColours_SizeId",
                table: "WatchColours");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "WatchColours");

            migrationBuilder.AddColumn<int>(
                name: "WatchCardId",
                table: "BrandLogos",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BrandLogos_WatchCardId",
                table: "BrandLogos",
                column: "WatchCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_BrandLogos_WatchCards_WatchCardId",
                table: "BrandLogos",
                column: "WatchCardId",
                principalTable: "WatchCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BrandLogos_WatchCards_WatchCardId",
                table: "BrandLogos");

            migrationBuilder.DropIndex(
                name: "IX_BrandLogos_WatchCardId",
                table: "BrandLogos");

            migrationBuilder.DropColumn(
                name: "WatchCardId",
                table: "BrandLogos");

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "WatchColours",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WatchColours_SizeId",
                table: "WatchColours",
                column: "SizeId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchColours_Sizes_SizeId",
                table: "WatchColours",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
