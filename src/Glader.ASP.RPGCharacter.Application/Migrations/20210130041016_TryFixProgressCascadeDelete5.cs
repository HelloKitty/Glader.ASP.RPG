using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class TryFixProgressCascadeDelete5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "DBRPGCharacterProgress<TestRaceType, TestClassType>Id",
                table: "character",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DBRPGCharacterProgress<TestRaceType, TestClassType>Id1",
                table: "character",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_DBRPGCharacterProgress<TestRaceType, TestClassType~",
                table: "character",
                column: "DBRPGCharacterProgress<TestRaceType, TestClassType>Id1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_character_character_progress_DBRPGCharacterProgress<TestRace~",
                table: "character",
                column: "DBRPGCharacterProgress<TestRaceType, TestClassType>Id1",
                principalTable: "character_progress",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_character_progress_DBRPGCharacterProgress<TestRace~",
                table: "character");

            migrationBuilder.DropIndex(
                name: "IX_character_DBRPGCharacterProgress<TestRaceType, TestClassType~",
                table: "character");

            migrationBuilder.DropColumn(
                name: "DBRPGCharacterProgress<TestRaceType, TestClassType>Id",
                table: "character");

            migrationBuilder.DropColumn(
                name: "DBRPGCharacterProgress<TestRaceType, TestClassType>Id1",
                table: "character");

            migrationBuilder.AddColumn<int>(
                name: "CharacterId",
                table: "character_progress",
                type: "int",
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
    }
}
