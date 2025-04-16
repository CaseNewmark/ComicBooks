using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComicBooks.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingSections : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Genres",
                table: "Sections");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Sections",
                newName: "Location");

            migrationBuilder.AlterColumn<Guid>(
                name: "FloorPlanId",
                table: "Sections",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FloorPlanId1",
                table: "Sections",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Genre",
                table: "Sections",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Sections_FloorPlanId1",
                table: "Sections",
                column: "FloorPlanId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_FloorPlans_FloorPlanId1",
                table: "Sections",
                column: "FloorPlanId1",
                principalTable: "FloorPlans",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sections_FloorPlans_FloorPlanId1",
                table: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_Sections_FloorPlanId1",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "FloorPlanId1",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Genre",
                table: "Sections");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "Sections",
                newName: "Name");

            migrationBuilder.AlterColumn<Guid>(
                name: "FloorPlanId",
                table: "Sections",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<string>(
                name: "Genres",
                table: "Sections",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
