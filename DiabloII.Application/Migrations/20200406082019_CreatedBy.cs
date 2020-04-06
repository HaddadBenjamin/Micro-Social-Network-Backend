using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabloII.Application.Migrations
{
    public partial class CreatedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ip",
                table: "SuggestionVotes");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "Ip",
                table: "SuggestionComments");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SuggestionVotes",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Suggestions",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "SuggestionComments",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SuggestionVotes");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Suggestions");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "SuggestionComments");

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "SuggestionVotes",
                maxLength: 15,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "Suggestions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ip",
                table: "SuggestionComments",
                maxLength: 15,
                nullable: false,
                defaultValue: "");
        }
    }
}
