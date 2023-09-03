using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.EntityFrameworkCore.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "DBO");

            migrationBuilder.CreateTable(
                name: "CLAIMS",
                schema: "DBO",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    NormalizedName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Status = table.Column<string>(type: "varchar(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CLAIMS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
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
                    table.PrimaryKey("PK_PRODUCTS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROLES",
                schema: "DBO",
                columns: table => new
                {
                    TenantCode = table.Column<string>(type: "varchar(50)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", nullable: false),
                    NormalizedName = table.Column<string>(type: "varchar(50)", nullable: false),
                    Status = table.Column<string>(type: "varchar(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ROLES_CLAIMS",
                schema: "DBO",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ClaimId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "varchar(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ROLES_CLAIMS", x => new { x.RoleId, x.ClaimId });
                });

            migrationBuilder.CreateTable(
                name: "USERS",
                schema: "DBO",
                columns: table => new
                {
                    TenantCode = table.Column<string>(type: "varchar(50)", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", nullable: false),
                    NormalizedUsername = table.Column<string>(type: "varchar(50)", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
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
                    table.PrimaryKey("PK_USERS", x => x.Id);
                });

            migrationBuilder.InsertData(
                schema: "DBO",
                table: "PRODUCTS",
                columns: new[] { "Id", "Category", "Code", "CreatedAt", "CreatedBy", "Explanation", "LastUpdatedAt", "LastUpdatedBy", "Name", "Price", "Status", "Stock", "TenantCode" },
                values: new object[,]
                {
                    { new Guid("0d42cccf-9bd2-43ed-b4d3-ade9644148ef"), "Category8", "Code8", new DateTime(2023, 9, 3, 10, 54, 40, 177, DateTimeKind.Utc).AddTicks(2854), "DEFAULT", "Explanation8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name8", 8m, "Active", 8, "DEFAULT" },
                    { new Guid("0de042a5-d7a3-40fd-a808-e3631b176e24"), "Category7", "Code7", new DateTime(2023, 9, 3, 10, 54, 40, 177, DateTimeKind.Utc).AddTicks(2844), "DEFAULT", "Explanation7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name7", 7m, "Active", 7, "DEFAULT" },
                    { new Guid("4675f71e-9e14-49f0-9a90-3433892d98c3"), "Category10", "Code10", new DateTime(2023, 9, 3, 10, 54, 40, 177, DateTimeKind.Utc).AddTicks(2884), "DEFAULT", "Explanation10", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name10", 10m, "Active", 10, "DEFAULT" },
                    { new Guid("4816af20-640f-49bc-9601-ca85634344eb"), "Category2", "Code2", new DateTime(2023, 9, 3, 10, 54, 40, 177, DateTimeKind.Utc).AddTicks(2789), "DEFAULT", "Explanation2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name2", 2m, "Active", 2, "DEFAULT" },
                    { new Guid("51372f8d-b5d7-453b-8f18-30b88ee4b5b2"), "Category1", "Code1", new DateTime(2023, 9, 3, 10, 54, 40, 177, DateTimeKind.Utc).AddTicks(2758), "DEFAULT", "Explanation1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name1", 1m, "Active", 1, "DEFAULT" },
                    { new Guid("89f9b507-b9f2-4c2b-bfb6-2a5f9589c597"), "Category4", "Code4", new DateTime(2023, 9, 3, 10, 54, 40, 177, DateTimeKind.Utc).AddTicks(2810), "DEFAULT", "Explanation4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name4", 4m, "Active", 4, "DEFAULT" },
                    { new Guid("bcc91754-8a41-43c7-b592-152b27695877"), "Category6", "Code6", new DateTime(2023, 9, 3, 10, 54, 40, 177, DateTimeKind.Utc).AddTicks(2834), "DEFAULT", "Explanation6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name6", 6m, "Active", 6, "DEFAULT" },
                    { new Guid("c5c5fd3d-43bd-46a1-91bb-09d30a22b752"), "Category3", "Code3", new DateTime(2023, 9, 3, 10, 54, 40, 177, DateTimeKind.Utc).AddTicks(2800), "DEFAULT", "Explanation3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name3", 3m, "Active", 3, "DEFAULT" },
                    { new Guid("d5fe22ac-d442-4522-a3a1-33d7aac1a37e"), "Category5", "Code5", new DateTime(2023, 9, 3, 10, 54, 40, 177, DateTimeKind.Utc).AddTicks(2819), "DEFAULT", "Explanation5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name5", 5m, "Active", 5, "DEFAULT" },
                    { new Guid("fa292c5c-2e50-4332-968c-bddc26fb360f"), "Category9", "Code9", new DateTime(2023, 9, 3, 10, 54, 40, 177, DateTimeKind.Utc).AddTicks(2869), "DEFAULT", "Explanation9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name9", 9m, "Active", 9, "DEFAULT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CLAIMS",
                schema: "DBO");

            migrationBuilder.DropTable(
                name: "PRODUCTS",
                schema: "DBO");

            migrationBuilder.DropTable(
                name: "ROLES",
                schema: "DBO");

            migrationBuilder.DropTable(
                name: "ROLES_CLAIMS",
                schema: "DBO");

            migrationBuilder.DropTable(
                name: "USERS",
                schema: "DBO");
        }
    }
}
