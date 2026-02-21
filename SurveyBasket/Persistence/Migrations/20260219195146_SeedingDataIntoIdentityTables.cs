using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SurveyBasket.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataIntoIdentityTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "IsDefault", "IsDeleted", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "19FAAB86-DF30-4853-9BC9-243B30887F65", "9B350BF4-A697-4FF0-BD33-79D3C32E3C50", false, false, "Admin", "ADMIN" },
                    { "8B51DB17-0E93-48E0-A701-FB962BDBAA26", "9B350BF4-A697-4FF0-BD33-79D3C32E3C50", true, false, "Member", "MEMBER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "48606D7C-F8DA-406D-B9B6-C1107E14A515", 0, "FC9634F6-FD78-4473-969B-AB0FA9465917", "admin@SarveyBsket.com", true, "Admin", "Default Account", false, null, "ADMIN@SARVEYBSKET.COM", "ADMIN@SARVEYBSKET.COM", "AQAAAAIAAYagAAAAENqaBNM8gVLSYfEdnQc8XYb/I2YIIS8gD1RVqyausCUQ0xt0LAzommCCVvD+dPBfPw==", "096873378763", true, "76F99E5872284B728AAA17AA39E1E66E", false, "admin@SarveyBsket.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "19FAAB86-DF30-4853-9BC9-243B30887F65", "48606D7C-F8DA-406D-B9B6-C1107E14A515" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8B51DB17-0E93-48E0-A701-FB962BDBAA26");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "19FAAB86-DF30-4853-9BC9-243B30887F65", "48606D7C-F8DA-406D-B9B6-C1107E14A515" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "19FAAB86-DF30-4853-9BC9-243B30887F65");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "48606D7C-F8DA-406D-B9B6-C1107E14A515");
        }
    }
}
