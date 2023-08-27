﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DBO");

            migrationBuilder.CreateTable(
                name: "PRODUCT",
                schema: "DBO",
                columns: table => new
                {
                    TenantCode = table.Column<string>(type: "varchar(50)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Code = table.Column<string>(type: "varchar(50)", nullable: false),
                    Name = table.Column<string>(type: "varchar(500)", nullable: false),
                    Category = table.Column<string>(type: "varchar(50)", nullable: false),
                    Explanation = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT", x => new { x.TenantCode, x.Id });
                });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUCT",
                schema: "DBO");
        }
    }
}
