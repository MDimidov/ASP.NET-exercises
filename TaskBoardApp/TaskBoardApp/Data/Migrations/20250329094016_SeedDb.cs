using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskBoardApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Boards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Board identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false, comment: "Board identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Task identifier")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false, comment: "Task title"),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, comment: "Task description"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "Task creation date"),
                    BoardId = table.Column<int>(type: "int", nullable: true, comment: "Board identifier"),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Application user identifier")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tasks_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                },
                comment: "Board Tasks");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "cb2aa0a0-f116-4cae-b6b5-daf2cf3c42bf", 0, "9a9856da-e846-4c94-86c6-ed650b4804b8", null, false, false, null, null, "TEST@SOFTUNI.BG", "AQAAAAIAAYagAAAAEMu6O4q0x6OAmstApiEinmqEjUCCxIlguPYsEe5IcCgWBqGOjHqupOx9RznjcRphYQ==", null, false, "dca6e98f-358a-4914-babf-df3fab825a92", false, "test@softuni.bg" });

            migrationBuilder.InsertData(
                table: "Boards",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Open" },
                    { 2, "In Progress" },
                    { 3, "Done" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "BoardId", "CreatedOn", "Description", "OwnerId", "Title" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 9, 10, 11, 40, 15, 425, DateTimeKind.Local).AddTicks(7466), "Implement better styling for all public pages", "cb2aa0a0-f116-4cae-b6b5-daf2cf3c42bf", "Improve CSS styles" },
                    { 2, 1, new DateTime(2025, 3, 24, 11, 40, 15, 425, DateTimeKind.Local).AddTicks(7529), "Create Android client app for the TaskBoard RESTful APIO", "cb2aa0a0-f116-4cae-b6b5-daf2cf3c42bf", "Android Client App" },
                    { 3, 2, new DateTime(2025, 3, 24, 11, 40, 15, 425, DateTimeKind.Local).AddTicks(7532), "Create Windows Forms desktop app client for the TaskBoard RESTful API", "cb2aa0a0-f116-4cae-b6b5-daf2cf3c42bf", "Desktop Client App" },
                    { 4, 3, new DateTime(2025, 3, 28, 11, 40, 15, 425, DateTimeKind.Local).AddTicks(7535), "Implement [Create Task] page for adding new tasks", "cb2aa0a0-f116-4cae-b6b5-daf2cf3c42bf", "Create Task" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_BoardId",
                table: "Tasks",
                column: "BoardId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_OwnerId",
                table: "Tasks",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Boards");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cb2aa0a0-f116-4cae-b6b5-daf2cf3c42bf");
        }
    }
}
