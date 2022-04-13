using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class BasketWatchedcreatedTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Count",
                table: "WatchCards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "BasketWatches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppUserId = table.Column<int>(nullable: false),
                    WatchId = table.Column<int>(nullable: false),
                    Count = table.Column<int>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    AppUserId1 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasketWatches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BasketWatches_AspNetUsers_AppUserId1",
                        column: x => x.AppUserId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BasketWatches_WatchCards_WatchId",
                        column: x => x.WatchId,
                        principalTable: "WatchCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BasketWatches_AppUserId1",
                table: "BasketWatches",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_BasketWatches_WatchId",
                table: "BasketWatches",
                column: "WatchId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BasketWatches");

            migrationBuilder.DropColumn(
                name: "Count",
                table: "WatchCards");
        }
    }
}
