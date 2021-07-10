using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class AddedItemQualityAndTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quality",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    VisualName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Proportion_R = table.Column<int>(nullable: true),
                    Proportion_G = table.Column<int>(nullable: true),
                    Proportion_B = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_quality", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "item_template",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClassId = table.Column<int>(nullable: false),
                    SubClassId = table.Column<int>(nullable: false),
                    VisualName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    QualityType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_item_template", x => x.Id);
                    table.ForeignKey(
                        name: "FK_item_template_quality_QualityType",
                        column: x => x.QualityType,
                        principalTable: "quality",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_item_template_item_sub_class_ClassId_SubClassId",
                        columns: x => new { x.ClassId, x.SubClassId },
                        principalTable: "item_sub_class",
                        principalColumns: new[] { "ItemClassId", "SubClassId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "quality",
                columns: new[] { "Id", "Description", "VisualName" },
                values: new object[,]
                {
                    { 1, "", "Poor" },
                    { 2, "", "Common" },
                    { 3, "", "Uncommon" },
                    { 4, "", "Rare" },
                    { 5, "", "Epic" },
                    { 6, "", "Legendary" },
                    { 7, "", "Mythical" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_item_template_QualityType",
                table: "item_template",
                column: "QualityType");

            migrationBuilder.CreateIndex(
                name: "IX_item_template_ClassId_SubClassId",
                table: "item_template",
                columns: new[] { "ClassId", "SubClassId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "item_template");

            migrationBuilder.DropTable(
                name: "quality");
        }
    }
}
