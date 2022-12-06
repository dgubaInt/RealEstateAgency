using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAgency.Infrastructure.Migrations
{
    public partial class RemoveMapTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estates_Maps_MapId",
                table: "Estates");

            migrationBuilder.DropTable(
                name: "Maps");

            migrationBuilder.DropIndex(
                name: "IX_Estates_MapId",
                table: "Estates");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("01ee81c3-acf7-4f08-85ef-44e4f56c53d5"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("e001ae6f-09c2-42e5-b078-eb5395a96e01"));

            migrationBuilder.DropColumn(
                name: "MapId",
                table: "Estates");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("1d1d6f9f-25c9-4127-94af-d5bb3967d0ff"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ddd60957-597d-4ffb-8ac9-e41f44e97803"));

            migrationBuilder.AddColumn<Guid>(
                name: "MapId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Maps",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstateAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MapName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maps", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEOijXeZjq9clVz9e5W2z/BiOo6Xm/MMZibc82N+u49Sot7d7LqWLUG9uTwrsVVtB7Q==", "a4f167e4-b5c2-4cb6-9aa4-5095cf8d63a4" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("01ee81c3-acf7-4f08-85ef-44e4f56c53d5"), "Rent", new DateTime(2022, 12, 1, 16, 18, 7, 16, DateTimeKind.Local).AddTicks(5662), null, 0 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("e001ae6f-09c2-42e5-b078-eb5395a96e01"), "Buy", new DateTime(2022, 12, 1, 16, 18, 7, 16, DateTimeKind.Local).AddTicks(5700), null, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Estates_MapId",
                table: "Estates",
                column: "MapId");

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_Maps_MapId",
                table: "Estates",
                column: "MapId",
                principalTable: "Maps",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
