using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class FixedQualityColorProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Proportion_R",
                table: "quality",
                newName: "Color_R");

            migrationBuilder.RenameColumn(
                name: "Proportion_G",
                table: "quality",
                newName: "Color_G");

            migrationBuilder.RenameColumn(
                name: "Proportion_B",
                table: "quality",
                newName: "Color_B");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Color_R",
                table: "quality",
                newName: "Proportion_R");

            migrationBuilder.RenameColumn(
                name: "Color_G",
                table: "quality",
                newName: "Proportion_G");

            migrationBuilder.RenameColumn(
                name: "Color_B",
                table: "quality",
                newName: "Proportion_B");
        }
    }
}
