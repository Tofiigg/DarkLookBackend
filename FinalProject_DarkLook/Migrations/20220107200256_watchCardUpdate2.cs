using Microsoft.EntityFrameworkCore.Migrations;

namespace FinalProject_DarkLook.Migrations
{
    public partial class watchCardUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "WatchCards",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "WatchCards",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescDetail",
                table: "WatchCards",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Stock",
                table: "WatchCards",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Brand",
                table: "WatchCards");

            migrationBuilder.DropColumn(
                name: "Code",
                table: "WatchCards");

            migrationBuilder.DropColumn(
                name: "DescDetail",
                table: "WatchCards");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "WatchCards");
        }
    }
}
