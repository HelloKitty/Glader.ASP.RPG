using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class AddedMap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "map",
                columns: table => new
                {
                    MapId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ContentPath = table.Column<string>(nullable: true),
                    EntryPoint_X = table.Column<float>(nullable: true),
                    EntryPoint_Y = table.Column<float>(nullable: true),
                    EntryPoint_Z = table.Column<float>(nullable: true),
                    VisualName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_map", x => x.MapId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "map");
        }
    }
}
