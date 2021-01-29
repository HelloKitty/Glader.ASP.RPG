using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class AddedRaceClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Class",
                table: "character",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Race",
                table: "character",
                nullable: false,
                defaultValue: 0);

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

            migrationBuilder.CreateIndex(
                name: "IX_character_Class",
                table: "character",
                column: "Class");

            migrationBuilder.CreateIndex(
                name: "IX_character_Race",
                table: "character",
                column: "Race");

            migrationBuilder.AddForeignKey(
                name: "FK_character_class_Class",
                table: "character",
                column: "Class",
                principalTable: "class",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_character_race_Race",
                table: "character",
                column: "Race",
                principalTable: "race",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_class_Class",
                table: "character");

            migrationBuilder.DropForeignKey(
                name: "FK_character_race_Race",
                table: "character");

            migrationBuilder.DropTable(
                name: "class");

            migrationBuilder.DropTable(
                name: "race");

            migrationBuilder.DropIndex(
                name: "IX_character_Class",
                table: "character");

            migrationBuilder.DropIndex(
                name: "IX_character_Race",
                table: "character");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "character");

            migrationBuilder.DropColumn(
                name: "Race",
                table: "character");
        }
    }
}
