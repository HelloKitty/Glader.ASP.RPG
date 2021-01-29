using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class AddedColorType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SlotColor_B",
                table: "character_customization_slot",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SlotColor_G",
                table: "character_customization_slot",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SlotColor_R",
                table: "character_customization_slot",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SlotColor_B",
                table: "character_customization_slot");

            migrationBuilder.DropColumn(
                name: "SlotColor_G",
                table: "character_customization_slot");

            migrationBuilder.DropColumn(
                name: "SlotColor_R",
                table: "character_customization_slot");
        }
    }
}
