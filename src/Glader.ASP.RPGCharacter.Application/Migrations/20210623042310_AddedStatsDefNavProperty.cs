using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class AddedStatsDefNavProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RPGStatDefinition<TestStatType>_stat_Stat",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RPGStatDefinition<TestStatType>",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropIndex(
                name: "IX_RPGStatDefinition<TestStatType>_Stat",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropColumn(
                name: "Stat",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.AddColumn<int>(
                name: "StatType",
                table: "RPGStatDefinition<TestStatType>",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RPGStatDefinition<TestStatType>",
                table: "RPGStatDefinition<TestStatType>",
                columns: new[] { "Level", "Race", "ClassId", "StatType" });

            migrationBuilder.CreateIndex(
                name: "IX_RPGStatDefinition<TestStatType>_StatType",
                table: "RPGStatDefinition<TestStatType>",
                column: "StatType");

            migrationBuilder.AddForeignKey(
                name: "FK_RPGStatDefinition<TestStatType>_stat_StatType",
                table: "RPGStatDefinition<TestStatType>",
                column: "StatType",
                principalTable: "stat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RPGStatDefinition<TestStatType>_stat_StatType",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RPGStatDefinition<TestStatType>",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropIndex(
                name: "IX_RPGStatDefinition<TestStatType>_StatType",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropColumn(
                name: "StatType",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.AddColumn<int>(
                name: "Stat",
                table: "RPGStatDefinition<TestStatType>",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RPGStatDefinition<TestStatType>",
                table: "RPGStatDefinition<TestStatType>",
                columns: new[] { "Level", "Race", "ClassId", "Stat" });

            migrationBuilder.CreateIndex(
                name: "IX_RPGStatDefinition<TestStatType>_Stat",
                table: "RPGStatDefinition<TestStatType>",
                column: "Stat");

            migrationBuilder.AddForeignKey(
                name: "FK_RPGStatDefinition<TestStatType>_stat_Stat",
                table: "RPGStatDefinition<TestStatType>",
                column: "Stat",
                principalTable: "stat",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
