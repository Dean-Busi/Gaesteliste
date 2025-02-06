using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class Identity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "122ee5da-5826-4876-bc1c-b37aebdeb2b7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2c5f66b0-ee1f-4cd6-9f46-5caa68a0ab5a");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3522d220-2322-41f4-8add-79df9904c8ad", null, "Admin", "ADMIN" },
                    { "4d63e76a-70e9-4187-b76d-49e4abc1f4a8", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3522d220-2322-41f4-8add-79df9904c8ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4d63e76a-70e9-4187-b76d-49e4abc1f4a8");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "122ee5da-5826-4876-bc1c-b37aebdeb2b7", null, "User", "USER" },
                    { "2c5f66b0-ee1f-4cd6-9f46-5caa68a0ab5a", null, "Admin", "ADMIN" }
                });
        }
    }
}
