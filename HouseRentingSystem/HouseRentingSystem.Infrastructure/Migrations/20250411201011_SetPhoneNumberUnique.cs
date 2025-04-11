using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HouseRentingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SetPhoneNumberUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5d2d3cbd-1afe-4015-9e8f-b7368410132c", "AQAAAAIAAYagAAAAEJ1BbUOS0/kCsQrZRSFxtpjbic13hIwt59Jgdn+HDZ+a1XplnWfybE1piMQCnvch9A==", "283b3d6a-ac2e-4d8c-b0af-933ec24377ab" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5f8b8f5d-f2d9-4e68-8d94-e95ef1856beb", "AQAAAAIAAYagAAAAEEXA+PQzBe6WAR0fyduglV8wLJw48jGYQWiywwARBxBv3LK6IqLm1NQDYWLoZW59HA==", "b08de850-7fa8-4085-94f7-e9c701c1eedb" });

            migrationBuilder.CreateIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents",
                column: "PhoneNumber",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Agents_PhoneNumber",
                table: "Agents");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d71609ed-dc49-447f-a55d-cb1f8ae38814", "AQAAAAIAAYagAAAAELqwNnzl0NHjr0Wp2fQosLcAAgvr7WKqoYYSowNF+BRvkzNujooJCB+F38dvKpeQnA==", "09a30362-adef-4d8c-a7a3-5b39af327c1d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "dea12856-c198-4129-b3f3-b893d8395082",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "65a34719-4d9a-4bc6-b2ac-d3d41044dd4f", "AQAAAAIAAYagAAAAEF+/ThMuDLLw7ADLK9jZ/toVeQntczdcMkOMW9G5tpmBHoeLb833BqQoumrj3/Vzew==", "f30a2f4b-e03f-4c0f-941e-a3449b8a804d" });
        }
    }
}
