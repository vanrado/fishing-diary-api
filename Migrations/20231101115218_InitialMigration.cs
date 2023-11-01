using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FishingDiaryAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Fisheries",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Title = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fisheries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FisheryUser",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                    fisheriesId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FisheryUser", x => new { x.UserId, x.fisheriesId });
                    table.ForeignKey(
                        name: "FK_FisheryUser_Fisheries_fisheriesId",
                        column: x => x.fisheriesId,
                        principalTable: "Fisheries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FisheryUser_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Fisheries",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { new Guid("d28888e9-2ba9-473a-a40f-e38cb54f9b35"), "VN Evička" },
                    { new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96"), "OR Melečka č. 1" }
                });

            migrationBuilder.InsertData(
                table: "User",
                column: "Id",
                value: new Guid("ebe94d5d-2ad8-4886-b246-05a1fad83d1c"));

            migrationBuilder.InsertData(
                table: "FisheryUser",
                columns: new[] { "UserId", "fisheriesId" },
                values: new object[] { new Guid("ebe94d5d-2ad8-4886-b246-05a1fad83d1c"), new Guid("da2fd609-d754-4feb-8acd-c4f9ff13ba96") });

            migrationBuilder.CreateIndex(
                name: "IX_FisheryUser_fisheriesId",
                table: "FisheryUser",
                column: "fisheriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FisheryUser");

            migrationBuilder.DropTable(
                name: "Fisheries");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
