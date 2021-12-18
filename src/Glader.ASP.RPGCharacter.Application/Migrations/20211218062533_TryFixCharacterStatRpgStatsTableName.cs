using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class TryFixCharacterStatRpgStatsTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RPGStatValue<TestStatType>_stat_StatType",
                table: "RPGStatValue<TestStatType>");

            migrationBuilder.DropForeignKey(
                name: "FK_RPGStatValue<TestStatType>_character_stat_default_Level_Race~",
                table: "RPGStatValue<TestStatType>");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RPGStatValue<TestStatType>",
                table: "RPGStatValue<TestStatType>");

            migrationBuilder.RenameTable(
                name: "RPGStatValue<TestStatType>",
                newName: "character_stat_default_rpgstats");

            migrationBuilder.RenameIndex(
                name: "IX_RPGStatValue<TestStatType>_StatType",
                table: "character_stat_default_rpgstats",
                newName: "IX_character_stat_default_rpgstats_StatType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_character_stat_default_rpgstats",
                table: "character_stat_default_rpgstats",
                columns: new[] { "Level", "Race", "ClassId", "StatType" });

            migrationBuilder.AddForeignKey(
                name: "FK_character_stat_default_rpgstats_stat_StatType",
                table: "character_stat_default_rpgstats",
                column: "StatType",
                principalTable: "stat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_character_stat_default_rpgstats_character_stat_default_Level~",
                table: "character_stat_default_rpgstats",
                columns: new[] { "Level", "Race", "ClassId" },
                principalTable: "character_stat_default",
                principalColumns: new[] { "Level", "RaceId", "ClassId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_stat_default_rpgstats_stat_StatType",
                table: "character_stat_default_rpgstats");

            migrationBuilder.DropForeignKey(
                name: "FK_character_stat_default_rpgstats_character_stat_default_Level~",
                table: "character_stat_default_rpgstats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_character_stat_default_rpgstats",
                table: "character_stat_default_rpgstats");

            migrationBuilder.RenameTable(
                name: "character_stat_default_rpgstats",
                newName: "RPGStatValue<TestStatType>");

            migrationBuilder.RenameIndex(
                name: "IX_character_stat_default_rpgstats_StatType",
                table: "RPGStatValue<TestStatType>",
                newName: "IX_RPGStatValue<TestStatType>_StatType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RPGStatValue<TestStatType>",
                table: "RPGStatValue<TestStatType>",
                columns: new[] { "Level", "Race", "ClassId", "StatType" });

            migrationBuilder.AddForeignKey(
                name: "FK_RPGStatValue<TestStatType>_stat_StatType",
                table: "RPGStatValue<TestStatType>",
                column: "StatType",
                principalTable: "stat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RPGStatValue<TestStatType>_character_stat_default_Level_Race~",
                table: "RPGStatValue<TestStatType>",
                columns: new[] { "Level", "Race", "ClassId" },
                principalTable: "character_stat_default",
                principalColumns: new[] { "Level", "RaceId", "ClassId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
