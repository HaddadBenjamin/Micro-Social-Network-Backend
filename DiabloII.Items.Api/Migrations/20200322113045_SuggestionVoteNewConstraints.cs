using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabloII.Items.Api.Migrations
{
    public partial class SuggestionVoteNewConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SuggestionVotes",
                table: "SuggestionVotes");

            migrationBuilder.DropIndex(
                name: "IX_SuggestionVotes_SuggestionId",
                table: "SuggestionVotes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuggestionVotes",
                table: "SuggestionVotes",
                columns: new[] { "SuggestionId", "Ip" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_SuggestionVotes",
                table: "SuggestionVotes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuggestionVotes",
                table: "SuggestionVotes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionVotes_SuggestionId",
                table: "SuggestionVotes",
                column: "SuggestionId");
        }
    }
}
