using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAgency.Infrastructure.Migrations
{
    public partial class SeedDataToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3d128fbb-2270-4b17-baf9-80a4eb7f9875", "a4c871a1-73cf-428f-90f7-a273ccd4740c", "admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c368d1b0-d46a-4b51-b737-c016faa91e31", "f67a998b-7268-4e31-ae32-370aaaf10efc", "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "7b9556cf-4db8-42c4-86bc-2abebc218ce9", 0, "4f743e54-0a01-46e1-a1bf-6f013f406211", "admin@gmail.com", false, false, null, "ADMIN@GMAIL.COM", "ADMIN", "AQAAAAEAACcQAAAAEEJSp/G8mWGPnA0tVq4TiGWgrW9F0b4z3WNU3k2WK58H+wZcTE1PCVmFokp7r7agpw==", null, false, "1de90cbc-8b53-45d3-b488-e985db5f9cca", false, "admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "3d128fbb-2270-4b17-baf9-80a4eb7f9875", "7b9556cf-4db8-42c4-86bc-2abebc218ce9" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c368d1b0-d46a-4b51-b737-c016faa91e31");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3d128fbb-2270-4b17-baf9-80a4eb7f9875", "7b9556cf-4db8-42c4-86bc-2abebc218ce9" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3d128fbb-2270-4b17-baf9-80a4eb7f9875");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "7b9556cf-4db8-42c4-86bc-2abebc218ce9");
        }
    }
}
