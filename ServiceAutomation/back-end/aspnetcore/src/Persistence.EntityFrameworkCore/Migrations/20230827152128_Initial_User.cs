using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial_User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("1829ae59-47ff-48c1-9cf0-7a709e48e048"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("34c56059-df6b-4d78-8d19-bab3c537a2e8"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("45b79499-3953-425e-bfa5-d01bd5a70435"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("8b56b056-3ad6-40f2-922e-f6ea2196316f"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("94645c20-ff23-432f-9b39-d836bd738839"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("abeaf34f-6763-4577-b07a-4d793d61f58e"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("d005f059-c7f7-468e-8746-21bfd3b1bf2c"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("d93a4754-e0ef-4a92-aa4c-7da6d8378733"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("ebf2d5d9-97b8-4f7e-ae2b-986470751063"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("fc8efe88-eb3f-4e5d-b4b8-97ac3ff5fb45"), "DEFAULT" });

            migrationBuilder.CreateTable(
                name: "USER",
                schema: "DBO",
                columns: table => new
                {
                    TenantCode = table.Column<string>(type: "varchar(50)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    Surname = table.Column<string>(type: "varchar(50)", nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(1000)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(100)", nullable: false),
                    Language = table.Column<string>(type: "varchar(20)", nullable: false),
                    Status = table.Column<string>(type: "varchar(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_USER", x => new { x.TenantCode, x.Id });
                });

            migrationBuilder.InsertData(
                schema: "DBO",
                table: "PRODUCT",
                columns: new[] { "Id", "TenantCode", "Category", "Code", "CreatedAt", "CreatedBy", "Explanation", "LastUpdatedAt", "LastUpdatedBy", "Name", "Price", "Status", "Stock" },
                values: new object[,]
                {
                    { new Guid("0d1312f1-8a9c-4eff-b816-bc92164d5e79"), "DEFAULT", "Category3", "Code3", new DateTime(2023, 8, 27, 15, 21, 28, 533, DateTimeKind.Utc).AddTicks(5591), "DEFAULT", "Explanation3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name3", 3m, "Active", 3 },
                    { new Guid("0edfcbff-bc66-4af5-927e-1fb549b04781"), "DEFAULT", "Category6", "Code6", new DateTime(2023, 8, 27, 15, 21, 28, 533, DateTimeKind.Utc).AddTicks(5623), "DEFAULT", "Explanation6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name6", 6m, "Active", 6 },
                    { new Guid("446dff11-a0af-4886-b18b-b665db1fc8bc"), "DEFAULT", "Category5", "Code5", new DateTime(2023, 8, 27, 15, 21, 28, 533, DateTimeKind.Utc).AddTicks(5610), "DEFAULT", "Explanation5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name5", 5m, "Active", 5 },
                    { new Guid("4d0f2405-a410-40d3-ae8d-d40f915c0826"), "DEFAULT", "Category7", "Code7", new DateTime(2023, 8, 27, 15, 21, 28, 533, DateTimeKind.Utc).AddTicks(5632), "DEFAULT", "Explanation7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name7", 7m, "Active", 7 },
                    { new Guid("5d079186-0c72-4368-b7e1-d2e9ac75202f"), "DEFAULT", "Category1", "Code1", new DateTime(2023, 8, 27, 15, 21, 28, 533, DateTimeKind.Utc).AddTicks(5558), "DEFAULT", "Explanation1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name1", 1m, "Active", 1 },
                    { new Guid("672076ca-3e9b-4a26-b851-730227d7102f"), "DEFAULT", "Category2", "Code2", new DateTime(2023, 8, 27, 15, 21, 28, 533, DateTimeKind.Utc).AddTicks(5582), "DEFAULT", "Explanation2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name2", 2m, "Active", 2 },
                    { new Guid("91f0e24f-beba-4325-8616-227cdb991e5e"), "DEFAULT", "Category8", "Code8", new DateTime(2023, 8, 27, 15, 21, 28, 533, DateTimeKind.Utc).AddTicks(5639), "DEFAULT", "Explanation8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name8", 8m, "Active", 8 },
                    { new Guid("959564a1-dd7c-483e-a2e8-4a455748d04c"), "DEFAULT", "Category10", "Code10", new DateTime(2023, 8, 27, 15, 21, 28, 533, DateTimeKind.Utc).AddTicks(5665), "DEFAULT", "Explanation10", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name10", 10m, "Active", 10 },
                    { new Guid("cdd66bac-8e92-4d4d-ba10-53cbb227ae8e"), "DEFAULT", "Category4", "Code4", new DateTime(2023, 8, 27, 15, 21, 28, 533, DateTimeKind.Utc).AddTicks(5601), "DEFAULT", "Explanation4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name4", 4m, "Active", 4 },
                    { new Guid("f1f62ade-f10b-42dc-8e62-a6697324c9be"), "DEFAULT", "Category9", "Code9", new DateTime(2023, 8, 27, 15, 21, 28, 533, DateTimeKind.Utc).AddTicks(5652), "DEFAULT", "Explanation9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name9", 9m, "Active", 9 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "USER",
                schema: "DBO");

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("0d1312f1-8a9c-4eff-b816-bc92164d5e79"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("0edfcbff-bc66-4af5-927e-1fb549b04781"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("446dff11-a0af-4886-b18b-b665db1fc8bc"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("4d0f2405-a410-40d3-ae8d-d40f915c0826"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("5d079186-0c72-4368-b7e1-d2e9ac75202f"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("672076ca-3e9b-4a26-b851-730227d7102f"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("91f0e24f-beba-4325-8616-227cdb991e5e"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("959564a1-dd7c-483e-a2e8-4a455748d04c"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("cdd66bac-8e92-4d4d-ba10-53cbb227ae8e"), "DEFAULT" });

            migrationBuilder.DeleteData(
                schema: "DBO",
                table: "PRODUCT",
                keyColumns: new[] { "Id", "TenantCode" },
                keyValues: new object[] { new Guid("f1f62ade-f10b-42dc-8e62-a6697324c9be"), "DEFAULT" });

            migrationBuilder.InsertData(
                schema: "DBO",
                table: "PRODUCT",
                columns: new[] { "Id", "TenantCode", "Category", "Code", "CreatedAt", "CreatedBy", "Explanation", "LastUpdatedAt", "LastUpdatedBy", "Name", "Price", "Status", "Stock" },
                values: new object[,]
                {
                    { new Guid("1829ae59-47ff-48c1-9cf0-7a709e48e048"), "DEFAULT", "Category1", "Code1", new DateTime(2023, 8, 27, 11, 54, 26, 197, DateTimeKind.Utc).AddTicks(8483), "DEFAULT", "Explanation1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name1", 1m, "Active", 1 },
                    { new Guid("34c56059-df6b-4d78-8d19-bab3c537a2e8"), "DEFAULT", "Category4", "Code4", new DateTime(2023, 8, 27, 11, 54, 26, 197, DateTimeKind.Utc).AddTicks(8515), "DEFAULT", "Explanation4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name4", 4m, "Active", 4 },
                    { new Guid("45b79499-3953-425e-bfa5-d01bd5a70435"), "DEFAULT", "Category6", "Code6", new DateTime(2023, 8, 27, 11, 54, 26, 197, DateTimeKind.Utc).AddTicks(8526), "DEFAULT", "Explanation6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name6", 6m, "Active", 6 },
                    { new Guid("8b56b056-3ad6-40f2-922e-f6ea2196316f"), "DEFAULT", "Category9", "Code9", new DateTime(2023, 8, 27, 11, 54, 26, 197, DateTimeKind.Utc).AddTicks(8539), "DEFAULT", "Explanation9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name9", 9m, "Active", 9 },
                    { new Guid("94645c20-ff23-432f-9b39-d836bd738839"), "DEFAULT", "Category3", "Code3", new DateTime(2023, 8, 27, 11, 54, 26, 197, DateTimeKind.Utc).AddTicks(8500), "DEFAULT", "Explanation3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name3", 3m, "Active", 3 },
                    { new Guid("abeaf34f-6763-4577-b07a-4d793d61f58e"), "DEFAULT", "Category7", "Code7", new DateTime(2023, 8, 27, 11, 54, 26, 197, DateTimeKind.Utc).AddTicks(8531), "DEFAULT", "Explanation7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name7", 7m, "Active", 7 },
                    { new Guid("d005f059-c7f7-468e-8746-21bfd3b1bf2c"), "DEFAULT", "Category2", "Code2", new DateTime(2023, 8, 27, 11, 54, 26, 197, DateTimeKind.Utc).AddTicks(8495), "DEFAULT", "Explanation2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name2", 2m, "Active", 2 },
                    { new Guid("d93a4754-e0ef-4a92-aa4c-7da6d8378733"), "DEFAULT", "Category8", "Code8", new DateTime(2023, 8, 27, 11, 54, 26, 197, DateTimeKind.Utc).AddTicks(8535), "DEFAULT", "Explanation8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name8", 8m, "Active", 8 },
                    { new Guid("ebf2d5d9-97b8-4f7e-ae2b-986470751063"), "DEFAULT", "Category5", "Code5", new DateTime(2023, 8, 27, 11, 54, 26, 197, DateTimeKind.Utc).AddTicks(8519), "DEFAULT", "Explanation5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name5", 5m, "Active", 5 },
                    { new Guid("fc8efe88-eb3f-4e5d-b4b8-97ac3ff5fb45"), "DEFAULT", "Category10", "Code10", new DateTime(2023, 8, 27, 11, 54, 26, 197, DateTimeKind.Utc).AddTicks(8545), "DEFAULT", "Explanation10", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name10", 10m, "Active", 10 }
                });
        }
    }
}
