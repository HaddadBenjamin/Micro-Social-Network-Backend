using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabloII.Application.Migrations
{
    public partial class SuggestionIp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Suggestions",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Suggestions");
        }
    }
}
