using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabloII.Infrastructure.Migrations
{
    public partial class SuggestionsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Suggestions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Content = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suggestions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SuggestionVotes",
                columns: table => new
                {
                    SuggestionId = table.Column<Guid>(nullable: false),
                    Ip = table.Column<string>(maxLength: 15, nullable: false),
                    Id = table.Column<Guid>(nullable: false),
                    IsPositive = table.Column<bool>(nullable: false),
                    SuggestionId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionVotes", x => new { x.SuggestionId, x.Ip });
                    table.UniqueConstraint("AK_SuggestionVotes_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionVotes_Suggestions_SuggestionId1",
                        column: x => x.SuggestionId1,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_Content",
                table: "Suggestions",
                column: "Content",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionVotes_Id",
                table: "SuggestionVotes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionVotes_SuggestionId1",
                table: "SuggestionVotes",
                column: "SuggestionId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuggestionVotes");

            migrationBuilder.DropTable(
                name: "Suggestions");
        }
    }
}
