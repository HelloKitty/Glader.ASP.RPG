using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class AddedSkillLevels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "character_skill_level",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    Skill = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Experience = table.Column<int>(nullable: false),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_skill_level", x => new { x.CharacterId, x.Skill });
                    table.ForeignKey(
                        name: "FK_character_skill_level_character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_skill_level_skill_Skill",
                        column: x => x.Skill,
                        principalTable: "skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_level_CharacterId",
                table: "character_skill_level",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_level_Skill",
                table: "character_skill_level",
                column: "Skill");

            migrationBuilder.AddForeignKey(
                name: "FK_character_skill_known_character_skill_level_CharacterId_Skill",
                table: "character_skill_known",
                columns: new[] { "CharacterId", "Skill" },
                principalTable: "character_skill_level",
                principalColumns: new[] { "CharacterId", "Skill" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_skill_known_character_skill_level_CharacterId_Skill",
                table: "character_skill_known");

            migrationBuilder.DropTable(
                name: "character_skill_level");
        }
    }
}
