using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class BrandAndNewsTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BrandLogos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(maxLength: 150, nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandLogos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "News",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(nullable: false),
                    Month = table.Column<string>(maxLength: 10, nullable: false),
                    Image = table.Column<string>(maxLength: 100, nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Desc = table.Column<string>(maxLength: 1000, nullable: false),
                    Info = table.Column<string>(maxLength: 1000, nullable: false),
                    RedInfo = table.Column<string>(maxLength: 1000, nullable: true),
                    SingleDesc = table.Column<string>(maxLength: 1010, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_News", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BrandLogos");

            migrationBuilder.DropTable(
                name: "News");
        }
    }
}
