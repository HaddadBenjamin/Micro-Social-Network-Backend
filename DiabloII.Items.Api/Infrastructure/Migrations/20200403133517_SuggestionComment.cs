using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabloII.Items.Api.Infrastructure.Migrations
{
    public partial class SuggestionComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuggestionVotes_Suggestions_SuggestionId1",
                table: "SuggestionVotes");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_SuggestionVotes_Id",
                table: "SuggestionVotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SuggestionVotes",
                table: "SuggestionVotes");

            migrationBuilder.DropIndex(
                name: "IX_SuggestionVotes_SuggestionId1",
                table: "SuggestionVotes");

            migrationBuilder.DropColumn(
                name: "SuggestionId1",
                table: "SuggestionVotes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuggestionVotes",
                table: "SuggestionVotes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "SuggestionComments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    SuggestionId = table.Column<Guid>(nullable: false),
                    Ip = table.Column<string>(maxLength: 15, nullable: false),
                    Comment = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuggestionComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SuggestionComments_Suggestions_SuggestionId",
                        column: x => x.SuggestionId,
                        principalTable: "Suggestions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionVotes_SuggestionId",
                table: "SuggestionVotes",
                column: "SuggestionId");

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionComments_Id",
                table: "SuggestionComments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionComments_SuggestionId",
                table: "SuggestionComments",
                column: "SuggestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_SuggestionVotes_Suggestions_SuggestionId",
                table: "SuggestionVotes",
                column: "SuggestionId",
                principalTable: "Suggestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuggestionVotes_Suggestions_SuggestionId",
                table: "SuggestionVotes");

            migrationBuilder.DropTable(
                name: "SuggestionComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SuggestionVotes",
                table: "SuggestionVotes");

            migrationBuilder.DropIndex(
                name: "IX_SuggestionVotes_SuggestionId",
                table: "SuggestionVotes");

            migrationBuilder.AddColumn<Guid>(
                name: "SuggestionId1",
                table: "SuggestionVotes",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_SuggestionVotes_Id",
                table: "SuggestionVotes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SuggestionVotes",
                table: "SuggestionVotes",
                columns: new[] { "SuggestionId", "Ip" });

            migrationBuilder.CreateIndex(
                name: "IX_SuggestionVotes_SuggestionId1",
                table: "SuggestionVotes",
                column: "SuggestionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_SuggestionVotes_Suggestions_SuggestionId1",
                table: "SuggestionVotes",
                column: "SuggestionId1",
                principalTable: "Suggestions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
