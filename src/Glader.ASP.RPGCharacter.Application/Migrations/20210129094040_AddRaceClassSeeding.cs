using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class AddRaceClassSeeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "class",
                columns: new[] { "Id", "Description", "VisualName" },
                values: new object[,]
                {
                    { 1, "", "Warrior" },
                    { 2, "", "Warlock" }
                });

            migrationBuilder.InsertData(
                table: "race",
                columns: new[] { "Id", "Description", "VisualName" },
                values: new object[,]
                {
                    { 1, "", "Human" },
                    { 2, "", "Orc" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "class",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "class",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "race",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "race",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
