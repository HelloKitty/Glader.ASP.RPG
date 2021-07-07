using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class AddedItemClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "item_class",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    VisualName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_class", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "item_class",
                columns: new[] { "Id", "Description", "VisualName" },
                values: new object[] { 1, "", "Weapon" });

            migrationBuilder.InsertData(
                table: "item_class",
                columns: new[] { "Id", "Description", "VisualName" },
                values: new object[] { 2, "", "Armor" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "item_class");
        }
    }
}
