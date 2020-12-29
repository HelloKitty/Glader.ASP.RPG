using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class RemovedUselessOwnershipId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_character_ownership",
                table: "character_ownership");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "character_ownership");

            migrationBuilder.AddPrimaryKey(
                name: "PK_character_ownership",
                table: "character_ownership",
                columns: new[] { "OwnershipId", "CharacterId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_character_ownership",
                table: "character_ownership");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "character_ownership",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_character_ownership",
                table: "character_ownership",
                column: "Id");
        }
    }
}
