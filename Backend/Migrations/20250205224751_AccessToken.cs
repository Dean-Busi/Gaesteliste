using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AccessToken : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "51fc86e9-01fe-4cdb-9145-b69b0993d028", null, "Admin", "ADMIN" },
                    { "c361bfa5-57ff-4eb4-b667-383828b2e61d", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "51fc86e9-01fe-4cdb-9145-b69b0993d028");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c361bfa5-57ff-4eb4-b667-383828b2e61d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3522d220-2322-41f4-8add-79df9904c8ad", null, "Admin", "ADMIN" },
                    { "4d63e76a-70e9-4187-b76d-49e4abc1f4a8", null, "User", "USER" }
                });
        }
    }
}
