using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class ColourRelationCreat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colour",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    WatchCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Colour_WatchCards_WatchCardId",
                        column: x => x.WatchCardId,
                        principalTable: "WatchCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WatchColour",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ColourId = table.Column<int>(nullable: true),
                    WatcCardId = table.Column<int>(nullable: false),
                    WatchCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchColour", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WatchColour_Colour_ColourId",
                        column: x => x.ColourId,
                        principalTable: "Colour",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WatchColour_WatchCards_WatchCardId",
                        column: x => x.WatchCardId,
                        principalTable: "WatchCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Colour_WatchCardId",
                table: "Colour",
                column: "WatchCardId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchColour_ColourId",
                table: "WatchColour",
                column: "ColourId");

            migrationBuilder.CreateIndex(
                name: "IX_WatchColour_WatchCardId",
                table: "WatchColour",
                column: "WatchCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchColour");

            migrationBuilder.DropTable(
                name: "Colour");
        }
    }
}
