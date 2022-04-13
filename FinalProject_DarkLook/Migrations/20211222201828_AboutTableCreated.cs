using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class AboutTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(maxLength: 200, nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: false),
                    Desc = table.Column<string>(maxLength: 1000, nullable: false),
                    Html = table.Column<string>(maxLength: 150, nullable: true),
                    Bootstrap = table.Column<string>(maxLength: 150, nullable: true),
                    Css = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abouts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OurTeams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(maxLength: 150, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Desc = table.Column<string>(maxLength: 500, nullable: false),
                    FaceBookUrl = table.Column<string>(maxLength: 150, nullable: true),
                    TwitterUrl = table.Column<string>(maxLength: 150, nullable: true),
                    PinterestUrl = table.Column<string>(maxLength: 150, nullable: true),
                    BeUrl = table.Column<string>(maxLength: 150, nullable: true),
                    Url = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OurTeams", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 150, nullable: false),
                    Percent = table.Column<string>(maxLength: 150, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Abouts");

            migrationBuilder.DropTable(
                name: "OurTeams");

            migrationBuilder.DropTable(
                name: "Skills");
        }
    }
}
