using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAgency.Infrastructure.Migrations
{
    public partial class UpdateCategoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("0879b1f4-e775-42b7-8b65-9e9a4398af56"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("574b5fcb-3c31-42bd-93fe-9cfe77bdc279"));

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEBJThn+Z3OaRlH+28qX7x46RDv1piH1eaxEBIKdAxUbsaG3P82pTToIScgO1qUo0Sw==", "fdfe980e-e158-48ac-adee-6f860f3253ee" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("10cd2eca-a8d1-4493-ab87-9905c5fc53d1"), "Buy", new DateTime(2022, 11, 23, 10, 48, 28, 320, DateTimeKind.Local).AddTicks(2417), null, 0 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("8f6b5907-f8e5-4413-b544-f264c939568f"), "Rent", new DateTime(2022, 11, 23, 10, 48, 28, 320, DateTimeKind.Local).AddTicks(2378), null, 0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("10cd2eca-a8d1-4493-ab87-9905c5fc53d1"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: new Guid("8f6b5907-f8e5-4413-b544-f264c939568f"));

            migrationBuilder.DropColumn(
                name: "Position",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAENBEb8CIxMt+UuVUiflMNaXjxNE+pfPmp/Ztb+0CJ2Sy+1U/k5dWiu/UrUew01GMeg==", "a409b1dd-2327-4f27-9d11-d768cb1414c0" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "CreatedDate", "ParentCategoryId" },
                values: new object[] { new Guid("0879b1f4-e775-42b7-8b65-9e9a4398af56"), "Rent", new DateTime(2022, 11, 21, 17, 13, 18, 445, DateTimeKind.Local).AddTicks(6195), null });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName", "CreatedDate", "ParentCategoryId" },
                values: new object[] { new Guid("574b5fcb-3c31-42bd-93fe-9cfe77bdc279"), "Buy", new DateTime(2022, 11, 21, 17, 13, 18, 445, DateTimeKind.Local).AddTicks(6235), null });
        }
    }
}
