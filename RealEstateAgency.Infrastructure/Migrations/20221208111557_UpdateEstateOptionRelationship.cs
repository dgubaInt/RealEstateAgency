using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAgency.Infrastructure.Migrations
{
    public partial class UpdateEstateOptionRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1d1d6f9f-25c9-4127-94af-d5bb3967d0ff"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ddd60957-597d-4ffb-8ac9-e41f44e97803"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEGl+VTO9cz+v3gENuWJjAdJGrTNZIt2n1AJeCdcGOGQMpcDg5jb14IFXdzPQr44nzg==", "f1b82c95-68ad-462e-b7c2-aa27411f5999" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("4a2d1a21-14a6-4e2c-9669-9b3af4390486"), "Rent", new DateTime(2022, 12, 8, 13, 15, 56, 601, DateTimeKind.Local).AddTicks(368), null, 0 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("bc051ec4-8d62-42cc-88f9-7df64b58e7a7"), "Buy", new DateTime(2022, 12, 8, 13, 15, 56, 601, DateTimeKind.Local).AddTicks(411), null, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("4a2d1a21-14a6-4e2c-9669-9b3af4390486"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("bc051ec4-8d62-42cc-88f9-7df64b58e7a7"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEA0IKXiKgbreGdcA3EKA3g1fBuLUOgtKGpU/KCOwdhIPOzcv3jH2UcYKdZheS3ruNg==", "3592450a-76cb-406c-9437-c83685aae113" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("1d1d6f9f-25c9-4127-94af-d5bb3967d0ff"), "Rent", new DateTime(2022, 12, 5, 15, 57, 25, 912, DateTimeKind.Local).AddTicks(3795), null, 0 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("ddd60957-597d-4ffb-8ac9-e41f44e97803"), "Buy", new DateTime(2022, 12, 5, 15, 57, 25, 912, DateTimeKind.Local).AddTicks(3838), null, 0 });
        }
    }
}
