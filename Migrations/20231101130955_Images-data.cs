using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FishingDiaryAPI.Migrations
{
    /// <inheritdoc />
    public partial class Imagesdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Fisheries",
                keyColumn: "Id",
                keyValue: new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"),
                column: "Images",
                value: "[\"https://www.fishsurfing.com/cdn/fspw-sk-images/30769/5d426de9.webp\",\"https://www.fishsurfing.com/cdn/fspw-sk-images/30769/7a0e641c.webp\"]");

            migrationBuilder.UpdateData(
                table: "Fisheries",
                keyColumn: "Id",
                keyValue: new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"),
                column: "Images",
                value: "[\"https://www.fishsurfing.com/cdn/fspw-sk-images/31274/09ce1094.webp\",\"https://www.fishsurfing.com/cdn/fspw-sk-images/31274/2731d261.webp\"]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}
