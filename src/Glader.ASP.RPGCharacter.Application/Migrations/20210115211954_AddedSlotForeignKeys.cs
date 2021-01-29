using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class AddedSlotForeignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_character_customization_slot_character_CharacterId",
                table: "character_customization_slot",
                column: "CharacterId",
                principalTable: "character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_character_proportion_slot_character_CharacterId",
                table: "character_proportion_slot",
                column: "CharacterId",
                principalTable: "character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_customization_slot_character_CharacterId",
                table: "character_customization_slot");

            migrationBuilder.DropForeignKey(
                name: "FK_character_proportion_slot_character_CharacterId",
                table: "character_proportion_slot");
        }
    }
}
