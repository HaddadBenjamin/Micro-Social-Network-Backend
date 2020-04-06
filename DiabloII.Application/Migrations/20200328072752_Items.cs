using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DiabloII.Application.Migrations
{
    public partial class Items : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Quality = table.Column<string>(nullable: false),
                    Category = table.Column<string>(nullable: false),
                    SubCategory = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    LevelRequired = table.Column<double>(nullable: false),
                    Level = table.Column<double>(nullable: false),
                    ImageName = table.Column<string>(nullable: true),
                    MinimumDefenseMinimum = table.Column<double>(nullable: false),
                    MaximumDefenseMinimum = table.Column<double>(nullable: false),
                    MinimumDefenseMaximum = table.Column<double>(nullable: false),
                    MaximumDefenseMaximum = table.Column<double>(nullable: false),
                    MinimumOneHandedDamageMinimum = table.Column<double>(nullable: false),
                    MaximumOneHandedDamageMinimum = table.Column<double>(nullable: false),
                    MinimumTwoHandedDamageMinimum = table.Column<double>(nullable: false),
                    MaximumTwoHandedDamageMinimum = table.Column<double>(nullable: false),
                    MinimumOneHandedDamageMaximum = table.Column<double>(nullable: false),
                    MaximumOneHandedDamageMaximum = table.Column<double>(nullable: false),
                    MinimumTwoHandedDamageMaximum = table.Column<double>(nullable: false),
                    MaximumTwoHandedDamageMaximum = table.Column<double>(nullable: false),
                    StrengthRequired = table.Column<double>(nullable: false),
                    DexterityRequired = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemProperties",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    FormattedName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Par = table.Column<double>(nullable: false),
                    Minimum = table.Column<double>(nullable: false),
                    Maximum = table.Column<double>(nullable: false),
                    IsPercent = table.Column<bool>(nullable: false),
                    FirstChararacter = table.Column<string>(nullable: true),
                    OrderIndex = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemProperties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemProperties_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemProperties_Id",
                table: "ItemProperties",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ItemProperties_ItemId",
                table: "ItemProperties",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Id",
                table: "Items",
                column: "Id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemProperties");

            migrationBuilder.DropTable(
                name: "Items");
        }
    }
}
