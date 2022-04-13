using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class WatchCardTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WatchCards",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(maxLength: 200, nullable: false),
                    HoverImage = table.Column<string>(maxLength: 200, nullable: true),
                    Desc = table.Column<string>(maxLength: 150, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IsNewArrivals = table.Column<bool>(nullable: false),
                    IsBestSeller = table.Column<bool>(nullable: false),
                    IsFeatured = table.Column<bool>(nullable: false),
                    IsDealsOftheWeek = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WatchCards", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WatchCards");
        }
    }
}
