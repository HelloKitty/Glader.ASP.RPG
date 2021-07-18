using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class AddedCharacterItemRaceClassDefaults : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "character_item_default",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "ClassId",
                table: "character_item_default",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RaceId",
                table: "character_item_default",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_item_default_ClassId",
                table: "character_item_default",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_character_item_default_RaceId",
                table: "character_item_default",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_character_item_default_class_ClassId",
                table: "character_item_default",
                column: "ClassId",
                principalTable: "class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_character_item_default_race_RaceId",
                table: "character_item_default",
                column: "RaceId",
                principalTable: "race",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_item_default_class_ClassId",
                table: "character_item_default");

            migrationBuilder.DropForeignKey(
                name: "FK_character_item_default_race_RaceId",
                table: "character_item_default");

            migrationBuilder.DropIndex(
                name: "IX_character_item_default_ClassId",
                table: "character_item_default");

            migrationBuilder.DropIndex(
                name: "IX_character_item_default_RaceId",
                table: "character_item_default");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "character_item_default");

            migrationBuilder.DropColumn(
                name: "ClassId",
                table: "character_item_default");

            migrationBuilder.DropColumn(
                name: "RaceId",
                table: "character_item_default");
        }
    }
}
