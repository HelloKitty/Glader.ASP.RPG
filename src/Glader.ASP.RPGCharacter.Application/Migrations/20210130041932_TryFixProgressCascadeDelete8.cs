using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class TryFixProgressCascadeDelete8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_character_progress_Id",
                table: "character");

            migrationBuilder.AddForeignKey(
                name: "FK_character_progress_character_Id",
                table: "character_progress",
                column: "Id",
                principalTable: "character",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_progress_character_Id",
                table: "character_progress");

            migrationBuilder.AddForeignKey(
                name: "FK_character_character_progress_Id",
                table: "character",
                column: "Id",
                principalTable: "character_progress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
