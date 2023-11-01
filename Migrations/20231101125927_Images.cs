using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishingDiaryAPI.Migrations
{
    /// <inheritdoc />
    public partial class Images : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Fisheries",
                type: "json",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Fisheries",
                keyColumn: "Id",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                column: "Images",
                value: "[]");

            migrationBuilder.UpdateData(
                table: "Fisheries",
                keyColumn: "Id",
                keyValue: new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                column: "Images",
                value: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Images",
                table: "Fisheries");
        }
    }
}
