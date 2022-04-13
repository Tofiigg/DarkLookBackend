using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class SizeRelationCreat : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchColour_Colour_ColourId",
                table: "WatchColour");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchColour_WatchCards_WatchCardId",
                table: "WatchColour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchColour",
                table: "WatchColour");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colour",
                table: "Colour");

            migrationBuilder.RenameTable(
                name: "WatchColour",
                newName: "WatchColours");

            migrationBuilder.RenameTable(
                name: "Colour",
                newName: "Colours");

            migrationBuilder.RenameIndex(
                name: "IX_WatchColour_WatchCardId",
                table: "WatchColours",
                newName: "IX_WatchColours_WatchCardId");

            migrationBuilder.RenameIndex(
                name: "IX_WatchColour_ColourId",
                table: "WatchColours",
                newName: "IX_WatchColours_ColourId");

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "WatchColours",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchColours",
                table: "WatchColours",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colours",
                table: "Colours",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Sizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sizes = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "watchSizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SizeId = table.Column<int>(nullable: true),
                    WatchCardId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_watchSizes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_watchSizes_Sizes_SizeId",
                        column: x => x.SizeId,
                        principalTable: "Sizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_watchSizes_WatchCards_WatchCardId",
                        column: x => x.WatchCardId,
                        principalTable: "WatchCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WatchColours_SizeId",
                table: "WatchColours",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_watchSizes_SizeId",
                table: "watchSizes",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_watchSizes_WatchCardId",
                table: "watchSizes",
                column: "WatchCardId");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchColours_Colours_ColourId",
                table: "WatchColours",
                column: "ColourId",
                principalTable: "Colours",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchColours_Sizes_SizeId",
                table: "WatchColours",
                column: "SizeId",
                principalTable: "Sizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchColours_WatchCards_WatchCardId",
                table: "WatchColours",
                column: "WatchCardId",
                principalTable: "WatchCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WatchColours_Colours_ColourId",
                table: "WatchColours");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchColours_Sizes_SizeId",
                table: "WatchColours");

            migrationBuilder.DropForeignKey(
                name: "FK_WatchColours_WatchCards_WatchCardId",
                table: "WatchColours");

            migrationBuilder.DropTable(
                name: "watchSizes");

            migrationBuilder.DropTable(
                name: "Sizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WatchColours",
                table: "WatchColours");

            migrationBuilder.DropIndex(
                name: "IX_WatchColours_SizeId",
                table: "WatchColours");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Colours",
                table: "Colours");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "WatchColours");

            migrationBuilder.RenameTable(
                name: "WatchColours",
                newName: "WatchColour");

            migrationBuilder.RenameTable(
                name: "Colours",
                newName: "Colour");

            migrationBuilder.RenameIndex(
                name: "IX_WatchColours_WatchCardId",
                table: "WatchColour",
                newName: "IX_WatchColour_WatchCardId");

            migrationBuilder.RenameIndex(
                name: "IX_WatchColours_ColourId",
                table: "WatchColour",
                newName: "IX_WatchColour_ColourId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WatchColour",
                table: "WatchColour",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Colour",
                table: "Colour",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WatchColour_Colour_ColourId",
                table: "WatchColour",
                column: "ColourId",
                principalTable: "Colour",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WatchColour_WatchCards_WatchCardId",
                table: "WatchColour",
                column: "WatchCardId",
                principalTable: "WatchCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
