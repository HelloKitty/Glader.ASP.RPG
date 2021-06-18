using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class AddedStatTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "stat",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    VisualName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stat", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "stat_default",
                columns: table => new
                {
                    Level = table.Column<int>(nullable: false),
                    RaceId = table.Column<int>(nullable: false),
                    ClassId = table.Column<int>(nullable: false)
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

            migrationBuilder.CreateTable(
                name: "RPGStatDefinition<TestStatType>",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    DBRPGStatDefaultTestStatTypeTestRaceTypeTestClassTypeClas = table.Column<int>(name: "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Clas~", nullable: false),
                    DBRPGStatDefaultTestStatTypeTestRaceTypeTestClassTypeLevel = table.Column<int>(name: "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Level", nullable: false),
                    DBRPGStatDefaultTestStatTypeTestRaceTypeTestClassTypeRace = table.Column<int>(name: "DBRPGStatDefault<TestStatType, TestRaceType, TestClassType>Race~", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPGStatDefinition<TestStatType>", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RPGStatDefinition<TestStatType>_stat_default_DBRPGStatDefaul~",
                        columns: x => new { x.DBRPGStatDefaultTestStatTypeTestRaceTypeTestClassTypeLevel, x.DBRPGStatDefaultTestStatTypeTestRaceTypeTestClassTypeRace, x.DBRPGStatDefaultTestStatTypeTestRaceTypeTestClassTypeClas },
                        principalTable: "stat_default",
                        principalColumns: new[] { "Level", "RaceId", "ClassId" },
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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropTable(
                name: "stat");

            migrationBuilder.DropTable(
                name: "stat_default");
        }
    }
}
