using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class TryFixCascadeDeleteSkillLevel2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_skill_level_character_skill_known_CharacterId_Skill",
                table: "character_skill_level");

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

            migrationBuilder.AddForeignKey(
                name: "FK_character_skill_level_character_skill_known_CharacterId_Skill",
                table: "character_skill_level",
                columns: new[] { "CharacterId", "Skill" },
                principalTable: "character_skill_known",
                principalColumns: new[] { "CharacterId", "Skill" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
