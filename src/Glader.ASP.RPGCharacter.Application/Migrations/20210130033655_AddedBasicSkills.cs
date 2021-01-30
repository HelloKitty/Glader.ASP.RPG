using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class AddedBasicSkills : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "skill",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    IsPassiveSkill = table.Column<bool>(nullable: false),
                    VisualName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skill", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "character_skill_known",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    Skill = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_skill_known", x => new { x.CharacterId, x.Skill });
                    table.ForeignKey(
                        name: "FK_character_skill_known_character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_skill_known_skill_Skill",
                        column: x => x.Skill,
                        principalTable: "skill",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "skill",
                columns: new[] { "Id", "Description", "IsPassiveSkill", "VisualName" },
                values: new object[,]
                {
                    { 1, "", false, "Woodcutting" },
                    { 2, "", false, "Mining" },
                    { 3, "", false, "Firemaking" },
                    { 4, "", false, "Parry" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_known_CharacterId",
                table: "character_skill_known",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_known_Skill",
                table: "character_skill_known",
                column: "Skill");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_skill_known");

            migrationBuilder.DropTable(
                name: "skill");
        }
    }
}
