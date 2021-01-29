using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class SwappedEnumValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "character_customization_slot_type",
                keyColumn: "SlotType",
                keyValue: 3,
                column: "VisualName",
                value: "Pants");

            migrationBuilder.UpdateData(
                table: "character_customization_slot_type",
                keyColumn: "SlotType",
                keyValue: 4,
                column: "VisualName",
                value: "Hair");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "character_customization_slot_type",
                keyColumn: "SlotType",
                keyValue: 3,
                column: "VisualName",
                value: "Hair");

            migrationBuilder.UpdateData(
                table: "character_customization_slot_type",
                keyColumn: "SlotType",
                keyValue: 4,
                column: "VisualName",
                value: "Pants");
        }
    }
}
