using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPG.Application.Migrations
{
    public partial class SeedStats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "stat",
                columns: new[] { "Id", "Description", "VisualName" },
                values: new object[] { 1, "", "Strength" });

            migrationBuilder.InsertData(
                table: "stat",
                columns: new[] { "Id", "Description", "VisualName" },
                values: new object[] { 2, "", "Intellect" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "stat",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "stat",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
