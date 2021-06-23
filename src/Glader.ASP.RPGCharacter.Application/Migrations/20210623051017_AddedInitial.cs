using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class AddedInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "character",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "character_customization_slot_type",
                columns: table => new
                {
                    SlotType = table.Column<int>(nullable: false),
                    VisualName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_customization_slot_type", x => x.SlotType);
                });

            migrationBuilder.CreateTable(
                name: "character_proportion_slot_type",
                columns: table => new
                {
                    SlotType = table.Column<int>(nullable: false),
                    VisualName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_proportion_slot_type", x => x.SlotType);
                });

            migrationBuilder.CreateTable(
                name: "class",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    VisualName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_class", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "group",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    CreationDate = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "race",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    VisualName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_race", x => x.Id);
                });

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
                name: "character_ownership",
                columns: table => new
                {
                    OwnershipId = table.Column<int>(nullable: false),
                    CharacterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_ownership", x => new { x.OwnershipId, x.CharacterId });
                    table.ForeignKey(
                        name: "FK_character_ownership_character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "character_progress",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Experience = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    PlayTime = table.Column<TimeSpan>(nullable: false, defaultValue: new TimeSpan(0, 0, 0, 0, 0)),
                    LastModifiedDate = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.ComputedColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_progress", x => x.Id);
                    table.ForeignKey(
                        name: "FK_character_progress_character_Id",
                        column: x => x.Id,
                        principalTable: "character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "character_customization_slot",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    SlotType = table.Column<int>(nullable: false),
                    CustomizationId = table.Column<int>(nullable: false),
                    SlotColor_R = table.Column<int>(nullable: true),
                    SlotColor_G = table.Column<int>(nullable: true),
                    SlotColor_B = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_customization_slot", x => new { x.CharacterId, x.SlotType });
                    table.ForeignKey(
                        name: "FK_character_customization_slot_character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_customization_slot_character_customization_slot_ty~",
                        column: x => x.SlotType,
                        principalTable: "character_customization_slot_type",
                        principalColumn: "SlotType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "character_proportion_slot",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    SlotType = table.Column<int>(nullable: false),
                    Proportion_X = table.Column<float>(nullable: true),
                    Proportion_Y = table.Column<float>(nullable: true),
                    Proportion_Z = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_proportion_slot", x => new { x.CharacterId, x.SlotType });
                    table.ForeignKey(
                        name: "FK_character_proportion_slot_character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_proportion_slot_character_proportion_slot_type_Slo~",
                        column: x => x.SlotType,
                        principalTable: "character_proportion_slot_type",
                        principalColumn: "SlotType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_member",
                columns: table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_member", x => x.CharacterId);
                    table.ForeignKey(
                        name: "FK_group_member_character_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_member_group_GroupId",
                        column: x => x.GroupId,
                        principalTable: "group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "character_definition",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Race = table.Column<int>(nullable: false),
                    Class = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_character_definition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_character_definition_class_Class",
                        column: x => x.Class,
                        principalTable: "class",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_definition_character_Id",
                        column: x => x.Id,
                        principalTable: "character",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_character_definition_race_Race",
                        column: x => x.Race,
                        principalTable: "race",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "RPGStatValue<TestStatType>",
                columns: table => new
                {
                    StatType = table.Column<int>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    Race = table.Column<int>(nullable: false),
                    ClassId = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RPGStatValue<TestStatType>", x => new { x.Level, x.Race, x.ClassId, x.StatType });
                    table.ForeignKey(
                        name: "FK_RPGStatValue<TestStatType>_stat_StatType",
                        column: x => x.StatType,
                        principalTable: "stat",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RPGStatValue<TestStatType>_character_stat_default_Level_Race~",
                        columns: x => new { x.Level, x.Race, x.ClassId },
                        principalTable: "character_stat_default",
                        principalColumns: new[] { "Level", "RaceId", "ClassId" },
                        onDelete: ReferentialAction.Cascade);
                });

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
                    table.ForeignKey(
                        name: "FK_character_skill_level_character_skill_known_CharacterId_Skill",
                        columns: x => new { x.CharacterId, x.Skill },
                        principalTable: "character_skill_known",
                        principalColumns: new[] { "CharacterId", "Skill" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "character_customization_slot_type",
                columns: new[] { "SlotType", "Description", "VisualName" },
                values: new object[,]
                {
                    { 0, "", "Shoes" },
                    { 1, "", "Feet" },
                    { 2, "", "Shirt" },
                    { 3, "", "Pants" },
                    { 4, "", "Hair" }
                });

            migrationBuilder.InsertData(
                table: "character_proportion_slot_type",
                columns: new[] { "SlotType", "Description", "VisualName" },
                values: new object[,]
                {
                    { 0, "", "Wrists" },
                    { 1, "", "Thighs" },
                    { 2, "", "Butt" }
                });

            migrationBuilder.InsertData(
                table: "class",
                columns: new[] { "Id", "Description", "VisualName" },
                values: new object[,]
                {
                    { 2, "", "Warlock" },
                    { 1, "", "Warrior" }
                });

            migrationBuilder.InsertData(
                table: "race",
                columns: new[] { "Id", "Description", "VisualName" },
                values: new object[,]
                {
                    { 1, "", "Human" },
                    { 2, "", "Orc" }
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

            migrationBuilder.InsertData(
                table: "stat",
                columns: new[] { "Id", "Description", "VisualName" },
                values: new object[,]
                {
                    { 1, "", "Strength" },
                    { 2, "", "Intellect" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_character_customization_slot_CharacterId",
                table: "character_customization_slot",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_character_customization_slot_SlotType",
                table: "character_customization_slot",
                column: "SlotType");

            migrationBuilder.CreateIndex(
                name: "IX_character_definition_Class",
                table: "character_definition",
                column: "Class");

            migrationBuilder.CreateIndex(
                name: "IX_character_definition_Race",
                table: "character_definition",
                column: "Race");

            migrationBuilder.CreateIndex(
                name: "IX_character_ownership_CharacterId",
                table: "character_ownership",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_character_ownership_OwnershipId",
                table: "character_ownership",
                column: "OwnershipId");

            migrationBuilder.CreateIndex(
                name: "IX_character_proportion_slot_CharacterId",
                table: "character_proportion_slot",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_character_proportion_slot_SlotType",
                table: "character_proportion_slot",
                column: "SlotType");

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_known_CharacterId",
                table: "character_skill_known",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_known_Skill",
                table: "character_skill_known",
                column: "Skill");

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_level_CharacterId",
                table: "character_skill_level",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_character_skill_level_Skill",
                table: "character_skill_level",
                column: "Skill");

            migrationBuilder.CreateIndex(
                name: "IX_character_stat_default_ClassId",
                table: "character_stat_default",
                column: "ClassId");

            migrationBuilder.CreateIndex(
                name: "IX_character_stat_default_RaceId",
                table: "character_stat_default",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_group_member_GroupId",
                table: "group_member",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_RPGStatValue<TestStatType>_StatType",
                table: "RPGStatValue<TestStatType>",
                column: "StatType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_customization_slot");

            migrationBuilder.DropTable(
                name: "character_definition");

            migrationBuilder.DropTable(
                name: "character_ownership");

            migrationBuilder.DropTable(
                name: "character_progress");

            migrationBuilder.DropTable(
                name: "character_proportion_slot");

            migrationBuilder.DropTable(
                name: "character_skill_level");

            migrationBuilder.DropTable(
                name: "group_member");

            migrationBuilder.DropTable(
                name: "RPGStatValue<TestStatType>");

            migrationBuilder.DropTable(
                name: "character_customization_slot_type");

            migrationBuilder.DropTable(
                name: "character_proportion_slot_type");

            migrationBuilder.DropTable(
                name: "character_skill_known");

            migrationBuilder.DropTable(
                name: "group");

            migrationBuilder.DropTable(
                name: "stat");

            migrationBuilder.DropTable(
                name: "character_stat_default");

            migrationBuilder.DropTable(
                name: "character");

            migrationBuilder.DropTable(
                name: "skill");

            migrationBuilder.DropTable(
                name: "class");

            migrationBuilder.DropTable(
                name: "race");
        }
    }
}
