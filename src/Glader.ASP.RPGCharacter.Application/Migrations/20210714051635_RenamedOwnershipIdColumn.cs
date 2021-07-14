using Microsoft.EntityFrameworkCore.Migrations;

namespace Glader.ASP.RPGCharacter.Application.Migrations
{
    public partial class RenamedOwnershipIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_item_inventory_item_instance_ownership_ItemInstanc~",
                table: "character_item_inventory");

            migrationBuilder.DropIndex(
                name: "IX_character_item_inventory_ItemInstanceOwnershipId",
                table: "character_item_inventory");

            migrationBuilder.DropIndex(
                name: "IX_character_item_inventory_ItemInstanceOwnershipId_OwnershipTy~",
                table: "character_item_inventory");

            migrationBuilder.DropColumn(
                name: "ItemInstanceOwnershipId",
                table: "character_item_inventory");

            migrationBuilder.AddColumn<int>(
                name: "OwnershipId",
                table: "character_item_inventory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_character_item_inventory_OwnershipId",
                table: "character_item_inventory",
                column: "OwnershipId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_item_inventory_OwnershipId_OwnershipType",
                table: "character_item_inventory",
                columns: new[] { "OwnershipId", "OwnershipType" });

            migrationBuilder.AddForeignKey(
                name: "FK_character_item_inventory_item_instance_ownership_OwnershipId~",
                table: "character_item_inventory",
                columns: new[] { "OwnershipId", "OwnershipType" },
                principalTable: "item_instance_ownership",
                principalColumns: new[] { "Id", "OwnershipType" },
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_character_item_inventory_item_instance_ownership_OwnershipId~",
                table: "character_item_inventory");

            migrationBuilder.DropIndex(
                name: "IX_character_item_inventory_OwnershipId",
                table: "character_item_inventory");

            migrationBuilder.DropIndex(
                name: "IX_character_item_inventory_OwnershipId_OwnershipType",
                table: "character_item_inventory");

            migrationBuilder.DropColumn(
                name: "OwnershipId",
                table: "character_item_inventory");

            migrationBuilder.AddColumn<int>(
                name: "ItemInstanceOwnershipId",
                table: "character_item_inventory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_character_item_inventory_ItemInstanceOwnershipId",
                table: "character_item_inventory",
                column: "ItemInstanceOwnershipId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_character_item_inventory_ItemInstanceOwnershipId_OwnershipTy~",
                table: "character_item_inventory",
                columns: new[] { "ItemInstanceOwnershipId", "OwnershipType" });

            migrationBuilder.AddForeignKey(
                name: "FK_character_item_inventory_item_instance_ownership_ItemInstanc~",
                table: "character_item_inventory",
                columns: new[] { "ItemInstanceOwnershipId", "OwnershipType" },
                principalTable: "item_instance_ownership",
                principalColumns: new[] { "Id", "OwnershipType" },
                onDelete: ReferentialAction.Cascade);
        }
    }
}
