using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class AddedProportions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                        name: "FK_character_proportion_slot_character_proportion_slot_type_Slo~",
                        column: x => x.SlotType,
                        principalTable: "character_proportion_slot_type",
                        principalColumn: "SlotType",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "character_proportion_slot_type",
                columns: new[] { "SlotType", "Description", "VisualName" },
                values: new object[] { 0, "", "Wrists" });

            migrationBuilder.InsertData(
                table: "character_proportion_slot_type",
                columns: new[] { "SlotType", "Description", "VisualName" },
                values: new object[] { 1, "", "Thighs" });

            migrationBuilder.InsertData(
                table: "character_proportion_slot_type",
                columns: new[] { "SlotType", "Description", "VisualName" },
                values: new object[] { 2, "", "Butt" });

            migrationBuilder.CreateIndex(
                name: "IX_character_proportion_slot_CharacterId",
                table: "character_proportion_slot",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_character_proportion_slot_SlotType",
                table: "character_proportion_slot",
                column: "SlotType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "character_proportion_slot");

            migrationBuilder.DropTable(
                name: "character_proportion_slot_type");
        }
    }
}
