using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class SettingTableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setting",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<string>(maxLength: 50, nullable: true),
                    WorkTimeDesc = table.Column<string>(maxLength: 200, nullable: true),
                    FacebookUrl = table.Column<string>(maxLength: 200, nullable: true),
                    GoogleUrl = table.Column<string>(maxLength: 200, nullable: true),
                    LinkUrl = table.Column<string>(maxLength: 200, nullable: true),
                    Twitter = table.Column<string>(maxLength: 200, nullable: true),
                    LastUrl = table.Column<string>(maxLength: 200, nullable: true),
                    Adress = table.Column<string>(maxLength: 200, nullable: true),
                    Street = table.Column<string>(maxLength: 200, nullable: true),
                    Number = table.Column<string>(maxLength: 50, nullable: true),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    WebUrl = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setting");
        }
    }
}
