using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class ChangedDefaultStatTableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RPGStatDefinition<TestStatType>_stat_default_DBRPGStatDefaul~",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropTable(
                name: "stat_default");

            migrationBuilder.DropIndex(
                name: "IX_RPGStatDefinition<TestStatType>_DBRPGStatDefault<TestStatTyp~",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropColumn(
                name: "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Clas~",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropColumn(
                name: "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Level",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropColumn(
                name: "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Race~",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.AddColumn<int>(
                name: "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClass~",
                table: "RPGStatDefinition<TestStatType>",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClas~1",
                table: "RPGStatDefinition<TestStatType>",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClas~2",
                table: "RPGStatDefinition<TestStatType>",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "character_stat_default",
                columns: table => new
                {
                    Level = table.Column<int>(nullable: false),
                    RaceId = table.Column<int>(nullable: false),
                    ClassId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_stat_default", x => new { x.Level, x.RaceId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_character_stat_default_class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_stat_default_race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RPGStatDefinition<TestStatType>_DBRPGCharacterStatDefault<Te~",
                table: "RPGStatDefinition<TestStatType>",
                columns: new[] { "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClas~1", "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClas~2", "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClass~" });

            migrationBuilder.CreateIndex(
                name: "IX_character_stat_default_ClassId",
                table: "character_stat_default",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_character_stat_default_RaceId",
                table: "character_stat_default",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RPGStatDefinition<TestStatType>_character_stat_default_DBRPG~",
                table: "RPGStatDefinition<TestStatType>",
                columns: new[] { "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClas~1", "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClas~2", "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClass~" },
                principalTable: "character_stat_default",
                principalColumns: new[] { "Level", "RaceId", "ClassId" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RPGStatDefinition<TestStatType>_character_stat_default_DBRPG~",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropTable(
                name: "character_stat_default");

            migrationBuilder.DropIndex(
                name: "IX_RPGStatDefinition<TestStatType>_DBRPGCharacterStatDefault<Te~",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropColumn(
                name: "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClass~",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropColumn(
                name: "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClas~1",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropColumn(
                name: "DBRPGCharacterStatDefault<TestStatType, TestRaceType, TestClas~2",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.AddColumn<int>(
                name: "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Clas~",
                table: "RPGStatDefinition<TestStatType>",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Level",
                table: "RPGStatDefinition<TestStatType>",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Race~",
                table: "RPGStatDefinition<TestStatType>",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "stat_default",
                columns: table => new
                {
                    Level = table.Column<int>(type: "int", nullable: false),
                    RaceId = table.Column<int>(type: "int", nullable: false),
                    ClassId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stat_default", x => new { x.Level, x.RaceId, x.ClassId });
                    table.ForeignKey(
                        name: "FK_stat_default_class_ClassId",
                        column: x => x.ClassId,
                        principalTable: "class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_stat_default_race_RaceId",
                        column: x => x.RaceId,
                        principalTable: "race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RPGStatDefinition<TestStatType>_DBRPGStatDefault<TestStatTyp~",
                table: "RPGStatDefinition<TestStatType>",
                columns: new[] { "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Level", "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Race~", "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Clas~" });

            migrationBuilder.CreateIndex(
                name: "IX_stat_default_ClassId",
                table: "stat_default",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_stat_default_RaceId",
                table: "stat_default",
                column: "RaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_RPGStatDefinition<TestStatType>_stat_default_DBRPGStatDefaul~",
                table: "RPGStatDefinition<TestStatType>",
                columns: new[] { "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Level", "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Race~", "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Clas~" },
                principalTable: "stat_default",
                principalColumns: new[] { "Level", "RaceId", "ClassId" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
