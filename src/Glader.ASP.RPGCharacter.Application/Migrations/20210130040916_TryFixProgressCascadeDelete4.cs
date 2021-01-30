using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class TryFixProgressCascadeDelete4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "character_progress",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_progress_CharacterId",
                table: "character_progress",
                column: "CharacterId");

            migrationBuilder.AddForeignKey(
                name: "FK_character_progress_character_CharacterId",
                table: "character_progress",
                column: "CharacterId",
                principalTable: "character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_progress_character_CharacterId",
                table: "character_progress");

            migrationBuilder.DropIndex(
                name: "IX_character_progress_CharacterId",
                table: "character_progress");

            migrationBuilder.DropColumn(
                name: "CharacterId",
                table: "character_progress");
        }
    }
}
