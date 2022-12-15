using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RealEstateAgency.Infrastructure.Migrations
{
    public partial class UpdateOnDeleteBehavior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estates_AspNetUsers_AgentUserId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_BuildingPlans_BuildingPlanId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_BuildingTypes_BuildingTypeId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_Categories_CategoryId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_EstateConditions_EstateConditionId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_Zones_ZoneId",
                table: "Estates");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("10532962-2fe3-4e57-9b5d-b9d31ff12b89"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("8fd2c18a-8a33-436f-8013-3fe14912c842"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ZoneId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "EstateConditionId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BuildingTypeId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "BuildingPlanId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<Guid>(
                name: "AgentUserId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAEMwHETIIE5M6NC/sDQSqcoMDKbAvzpE6yb2oJ3i0lnrIPLMP0w6xu+5Rc+5XAwL60g==", "657b8bc1-d9f3-4f16-a977-74e0841675b1" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("3acee8d4-abe9-4fc5-af5d-0a981e6c2c57"), "Buy", new DateTime(2022, 12, 13, 15, 51, 44, 451, DateTimeKind.Local).AddTicks(3293), null, 0 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("971da4f9-5854-4735-b3f9-c85f773d0688"), "Rent", new DateTime(2022, 12, 13, 15, 51, 44, 451, DateTimeKind.Local).AddTicks(3250), null, 0 });

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_AspNetUsers_AgentUserId",
                table: "Estates",
                column: "AgentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_BuildingPlans_BuildingPlanId",
                table: "Estates",
                column: "BuildingPlanId",
                principalTable: "BuildingPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_BuildingTypes_BuildingTypeId",
                table: "Estates",
                column: "BuildingTypeId",
                principalTable: "BuildingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_Categories_CategoryId",
                table: "Estates",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_EstateConditions_EstateConditionId",
                table: "Estates",
                column: "EstateConditionId",
                principalTable: "EstateConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_Zones_ZoneId",
                table: "Estates",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estates_AspNetUsers_AgentUserId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_BuildingPlans_BuildingPlanId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_BuildingTypes_BuildingTypeId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_Categories_CategoryId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_EstateConditions_EstateConditionId",
                table: "Estates");

            migrationBuilder.DropForeignKey(
                name: "FK_Estates_Zones_ZoneId",
                table: "Estates");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("3acee8d4-abe9-4fc5-af5d-0a981e6c2c57"));

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: new Guid("971da4f9-5854-4735-b3f9-c85f773d0688"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ZoneId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EstateConditionId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "CategoryId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BuildingTypeId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "BuildingPlanId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AgentUserId",
                table: "Estates",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("7b9556cf-4db8-42c4-86bc-2abebc218ce9"),
                columns: new[] { "PasswordHash", "SecurityStamp" },
                values: new object[] { "AQAAAAEAACcQAAAAENBiD2bFqd5MtgzP1OWkbiG4Ssq2qgTJoqFdq/zwQ/QXZhI4i4aZZpLYspunNcs02w==", "b8d33647-c645-4555-be00-cebd926f2bef" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("10532962-2fe3-4e57-9b5d-b9d31ff12b89"), "Rent", new DateTime(2022, 12, 8, 13, 23, 21, 487, DateTimeKind.Local).AddTicks(5795), null, 0 });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CategoryName", "CreatedDate", "ParentCategoryId", "Position" },
                values: new object[] { new Guid("8fd2c18a-8a33-436f-8013-3fe14912c842"), "Buy", new DateTime(2022, 12, 8, 13, 23, 21, 487, DateTimeKind.Local).AddTicks(5841), null, 0 });

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_AspNetUsers_AgentUserId",
                table: "Estates",
                column: "AgentUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_BuildingPlans_BuildingPlanId",
                table: "Estates",
                column: "BuildingPlanId",
                principalTable: "BuildingPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_BuildingTypes_BuildingTypeId",
                table: "Estates",
                column: "BuildingTypeId",
                principalTable: "BuildingTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_Categories_CategoryId",
                table: "Estates",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_EstateConditions_EstateConditionId",
                table: "Estates",
                column: "EstateConditionId",
                principalTable: "EstateConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Estates_Zones_ZoneId",
                table: "Estates",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
