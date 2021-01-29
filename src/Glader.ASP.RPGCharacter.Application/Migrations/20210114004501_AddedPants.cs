using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class AddedPants : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "character_customization_slot_type",
                columns: new[] { "SlotType", "Description", "VisualName" },
                values: new object[] { 4, "", "Pants" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "character_customization_slot_type",
                keyColumn: "SlotType",
                keyValue: 4);
        }
    }
}
