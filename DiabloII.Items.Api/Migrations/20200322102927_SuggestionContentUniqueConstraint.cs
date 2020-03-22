using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabloII.Items.Api.Migrations
{
    public partial class SuggestionContentUniqueConstraint : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suggestions_Content",
                table: "Suggestions");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_Content",
                table: "Suggestions",
                column: "Content",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suggestions_Content",
                table: "Suggestions");

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_Content",
                table: "Suggestions",
                column: "Content");
        }
    }
}
