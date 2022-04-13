using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class AboutTableUpdate1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Profession",
                table: "Abouts",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Profession",
                table: "Abouts");
        }
    }
}
