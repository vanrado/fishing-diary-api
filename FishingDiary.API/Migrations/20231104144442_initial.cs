using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FishingDiary.API.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fisheries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Images = table.Column<string>(type: "json", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fisheries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserFisheries",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FisheryId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFisheries", x => new { x.UserId, x.FisheryId });
                    table.ForeignKey(
                        name: "FK_UserFisheries_Fisheries_FisheryId",
                        column: x => x.FisheryId,
                        principalTable: "Fisheries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Fisheries",
                columns: new[] { "Id", "Images", "Title" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "[\"https://www.fishsurfing.com/cdn/fspw-sk-images/30769/5d426de9.webp\",\"https://www.fishsurfing.com/cdn/fspw-sk-images/30769/7a0e641c.webp\"]", "VN Evička" },
                    { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "[\"https://www.fishsurfing.com/cdn/fspw-sk-images/31274/09ce1094.webp\",\"https://www.fishsurfing.com/cdn/fspw-sk-images/31274/2731d261.webp\"]", "OR Melečka č. 1" }
                });

            migrationBuilder.InsertData(
                table: "UserFisheries",
                columns: new[] { "FisheryId", "UserId", "Id" },
                values: new object[] { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), new Guid("12345678-1234-5678-1234-567812345678"), new Guid("39de575e-61a0-4b8c-8762-6c9bcd43f3ec") });

            migrationBuilder.CreateIndex(
                name: "IX_UserFisheries_FisheryId",
                table: "UserFisheries",
                column: "FisheryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserFisheries");

            migrationBuilder.DropTable(
                name: "Fisheries");
        }
    }
}
