using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAgency.Infrastructure.Migrations
{
    public partial class UpdateTagColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3fb90093-9945-470e-915e-22b57abd86fd"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("ca1fba20-fdd9-4d4b-8c06-44b92eb2fef9"));

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEAV9LMmte1SW+Yez3YudqEuw/xGvOLjgI/eH9p4zUb0NkzhJhqazGhc2NxWPoG6W+A==", "8a39600e-ec14-4e76-9af9-c948f45be380" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("535efb21-3c39-4cae-b302-5a176cfdd9d6"), "Rent", new DateTime(2022, 12, 23, 16, 24, 53, 494, DateTimeKind.Local).AddTicks(3792), null, 0 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("5bfdfd10-6796-4653-a614-18e4dc1fac20"), "Buy", new DateTime(2022, 12, 23, 16, 24, 53, 494, DateTimeKind.Local).AddTicks(3834), null, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("535efb21-3c39-4cae-b302-5a176cfdd9d6"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("5bfdfd10-6796-4653-a614-18e4dc1fac20"));

            migrationBuilder.AlterColumn<string>(
                name: "Tags",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEAVOyrjoZMh4xkKGCWh9bH9oYCOIdR9MoQSnY2VjAOom2JOloXCzHifQfvKez3YYLA==", "d98c598b-334b-4b02-bc1d-e3a3d40503a6" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("3fb90093-9945-470e-915e-22b57abd86fd"), "Buy", new DateTime(2022, 12, 23, 16, 23, 22, 401, DateTimeKind.Local).AddTicks(2905), null, 0 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("ca1fba20-fdd9-4d4b-8c06-44b92eb2fef9"), "Rent", new DateTime(2022, 12, 23, 16, 23, 22, 401, DateTimeKind.Local).AddTicks(2860), null, 0 });
        }
    }
}
