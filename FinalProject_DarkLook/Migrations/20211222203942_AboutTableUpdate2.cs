using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class AboutTableUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Abouts");

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "OurTeams",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "OurTeams");

            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Abouts",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
