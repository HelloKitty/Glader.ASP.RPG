using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class UpdatedRPGStatDef : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RPGStatDefinition<TestStatType>",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.AddColumn<int>(
                name: "Key",
                table: "RPGStatDefinition<TestStatType>",
                nullable: false,
                defaultValue: 0)
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RPGStatDefinition<TestStatType>",
                table: "RPGStatDefinition<TestStatType>",
                column: "Key");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RPGStatDefinition<TestStatType>",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "RPGStatDefinition<TestStatType>");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RPGStatDefinition<TestStatType>",
                table: "RPGStatDefinition<TestStatType>",
                column: "Id");
        }
    }
}
