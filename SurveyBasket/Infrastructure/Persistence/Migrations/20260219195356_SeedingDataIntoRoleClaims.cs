using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SurveyBasket.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataIntoRoleClaims : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoleClaims",
                columns: new[] { "Id", "ClaimType", "ClaimValue", "RoleId" },
                values: new object[,]
                {
                    { 1, "Permissions", "polls:read", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 2, "Permissions", "polls:add", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 3, "Permissions", "polls:update", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 4, "Permissions", "polls:delete", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 5, "Permissions", "questions:read", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 6, "Permissions", "questions:add", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 7, "Permissions", "questions:update", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 8, "Permissions", "users:read", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 9, "Permissions", "users:add", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 10, "Permissions", "users:update", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 11, "Permissions", "roles:read", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 12, "Permissions", "roles:add", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 13, "Permissions", "roles:update", "19FAAB86-DF30-4853-9BC9-243B30887F65" },
                    { 14, "Permissions", "dashboard:read", "19FAAB86-DF30-4853-9BC9-243B30887F65" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "AspNetRoleClaims",
                keyColumn: "Id",
                keyValue: 14);
        }
    }
}
