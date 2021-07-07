using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class AddedItemSubclasses : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "item_sub_class",
                columns: table => new
                {
                    SubClassId = table.Column<int>(nullable: false),
                    ItemClassId = table.Column<int>(nullable: false),
                    VisualName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_sub_class", x => new { x.ItemClassId, x.SubClassId });
                    table.ForeignKey(
                        name: "FK_item_sub_class_item_class_ItemClassId",
                        column: x => x.ItemClassId,
                        principalTable: "item_class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "item_sub_class");
        }
    }
}
