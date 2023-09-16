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
                name: "TENANTS",
                schema: "DBO",
                columns: table => new
                {
                    TenantCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "varchar(10)", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "varchar(50)", nullable: false),
                    LastUpdatedAt = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TENANTS", x => x.TenantCode);
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
                table: "CLAIMS",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "NormalizedName", "Status" },
                values: new object[,]
                {
                    { new Guid("302b0514-4bd6-4d5f-9495-9bf129070423"), new DateTime(2023, 9, 16, 17, 37, 43, 534, DateTimeKind.Local).AddTicks(1106), "SYSTEM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "claim.create", "CLAIM.CREATE", "Active" },
                    { new Guid("6df72ae0-ecb1-46cf-8208-41614d48e732"), new DateTime(2023, 9, 16, 17, 37, 43, 534, DateTimeKind.Local).AddTicks(1129), "SYSTEM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "claim.get", "CLAIM.GET", "Active" },
                    { new Guid("af80beb8-1e62-4a71-8791-a29ea9f728f0"), new DateTime(2023, 9, 16, 17, 37, 43, 534, DateTimeKind.Local).AddTicks(1132), "SYSTEM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "system.role.create", "SYSTEM.ROLE.CREATE", "Active" }
                });

            migrationBuilder.InsertData(
                schema: "DBO",
                table: "PRODUCTS",
                columns: new[] { "Id", "Category", "Code", "CreatedAt", "CreatedBy", "Explanation", "LastUpdatedAt", "LastUpdatedBy", "Name", "Price", "Status", "Stock", "TenantCode" },
                values: new object[,]
                {
                    { new Guid("063618cb-f454-42e6-9176-4bb71fe130ac"), "Category2", "Code2", new DateTime(2023, 9, 16, 14, 37, 43, 939, DateTimeKind.Utc).AddTicks(9981), "DEFAULT", "Explanation2", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name2", 2m, "Active", 2, "DEFAULT" },
                    { new Guid("27c9aec1-6954-4174-bbab-833d5d5af93f"), "Category3", "Code3", new DateTime(2023, 9, 16, 14, 37, 43, 939, DateTimeKind.Utc).AddTicks(9987), "DEFAULT", "Explanation3", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name3", 3m, "Active", 3, "DEFAULT" },
                    { new Guid("48e7c59b-5a58-4e62-b46c-a94e850aee24"), "Category8", "Code8", new DateTime(2023, 9, 16, 14, 37, 43, 940, DateTimeKind.Utc).AddTicks(97), "DEFAULT", "Explanation8", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name8", 8m, "Active", 8, "DEFAULT" },
                    { new Guid("5ffc1b31-11c9-4bc8-95c4-de99f896f300"), "Category6", "Code6", new DateTime(2023, 9, 16, 14, 37, 43, 940, DateTimeKind.Utc).AddTicks(87), "DEFAULT", "Explanation6", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name6", 6m, "Active", 6, "DEFAULT" },
                    { new Guid("6cab53f5-07e5-4b02-a7ed-d28b92886af2"), "Category4", "Code4", new DateTime(2023, 9, 16, 14, 37, 43, 940, DateTimeKind.Utc).AddTicks(56), "DEFAULT", "Explanation4", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name4", 4m, "Active", 4, "DEFAULT" },
                    { new Guid("84c41c96-a966-4c02-8dfb-a7f3a2ed0722"), "Category10", "Code10", new DateTime(2023, 9, 16, 14, 37, 43, 940, DateTimeKind.Utc).AddTicks(109), "DEFAULT", "Explanation10", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name10", 10m, "Active", 10, "DEFAULT" },
                    { new Guid("9ecfc4c9-01e0-4f72-a953-2535812e2d6b"), "Category7", "Code7", new DateTime(2023, 9, 16, 14, 37, 43, 940, DateTimeKind.Utc).AddTicks(92), "DEFAULT", "Explanation7", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name7", 7m, "Active", 7, "DEFAULT" },
                    { new Guid("d08fa985-7dda-4869-88be-ed512274025d"), "Category1", "Code1", new DateTime(2023, 9, 16, 14, 37, 43, 939, DateTimeKind.Utc).AddTicks(9971), "DEFAULT", "Explanation1", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name1", 1m, "Active", 1, "DEFAULT" },
                    { new Guid("fcd7ca19-8904-4915-a01d-f7dd49a4b5f9"), "Category9", "Code9", new DateTime(2023, 9, 16, 14, 37, 43, 940, DateTimeKind.Utc).AddTicks(102), "DEFAULT", "Explanation9", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name9", 9m, "Active", 9, "DEFAULT" },
                    { new Guid("fdafe6b2-e733-4eb2-9f25-8394875f42bc"), "Category5", "Code5", new DateTime(2023, 9, 16, 14, 37, 43, 940, DateTimeKind.Utc).AddTicks(73), "DEFAULT", "Explanation5", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Name5", 5m, "Active", 5, "DEFAULT" }
                });

            migrationBuilder.InsertData(
                schema: "DBO",
                table: "ROLES",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Name", "NormalizedName", "Status", "TenantCode" },
                values: new object[] { new Guid("a06310b2-5b34-4760-8b1b-33ec48dd3cfa"), new DateTime(2023, 9, 16, 17, 37, 43, 534, DateTimeKind.Local).AddTicks(6567), "SYSTEM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "admin", "ADMIN", "Active", "SYSTEM" });

            migrationBuilder.InsertData(
                schema: "DBO",
                table: "ROLES_CLAIMS",
                columns: new[] { "ClaimId", "RoleId", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Status" },
                values: new object[,]
                {
                    { new Guid("302b0514-4bd6-4d5f-9495-9bf129070423"), new Guid("a06310b2-5b34-4760-8b1b-33ec48dd3cfa"), new DateTime(2023, 9, 16, 17, 37, 43, 535, DateTimeKind.Local).AddTicks(4655), "SYSTEM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Active" },
                    { new Guid("6df72ae0-ecb1-46cf-8208-41614d48e732"), new Guid("a06310b2-5b34-4760-8b1b-33ec48dd3cfa"), new DateTime(2023, 9, 16, 17, 37, 43, 535, DateTimeKind.Local).AddTicks(4670), "SYSTEM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Active" },
                    { new Guid("af80beb8-1e62-4a71-8791-a29ea9f728f0"), new Guid("a06310b2-5b34-4760-8b1b-33ec48dd3cfa"), new DateTime(2023, 9, 16, 17, 37, 43, 535, DateTimeKind.Local).AddTicks(4678), "SYSTEM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Active" }
                });

            migrationBuilder.InsertData(
                schema: "DBO",
                table: "TENANTS",
                columns: new[] { "TenantCode", "CreatedAt", "CreatedBy", "LastUpdatedAt", "LastUpdatedBy", "Status" },
                values: new object[,]
                {
                    { "AYTAS", new DateTime(2023, 9, 16, 14, 37, 43, 537, DateTimeKind.Utc).AddTicks(5402), "SYSTEM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Active" },
                    { "SYSTEM", new DateTime(2023, 9, 16, 14, 37, 43, 537, DateTimeKind.Utc).AddTicks(5398), "SYSTEM", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "Active" }
                });

            migrationBuilder.InsertData(
                schema: "DBO",
                table: "USERS",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "LastUpdatedAt", "LastUpdatedBy", "Name", "NormalizedUsername", "PasswordHash", "PasswordSalt", "RoleId", "Status", "Surname", "TenantCode", "Username" },
                values: new object[] { new Guid("40eb7d2f-5969-4afa-90a4-84aa0377fc04"), new DateTime(2023, 9, 16, 17, 37, 43, 938, DateTimeKind.Local).AddTicks(9440), "SYSTEM", "system@gmail.com", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", "System", "SYSTEMUSER", new byte[] { 211, 220, 92, 79, 103, 82, 152, 95, 228, 181, 100, 144, 18, 117, 225, 117, 198, 111, 99, 84, 34, 227, 50, 145, 89, 26, 183, 126, 194, 210, 128, 249, 12, 119, 93, 24, 73, 0, 61, 158, 115, 142, 250, 115, 147, 243, 182, 212, 154, 188, 196, 175, 143, 110, 88, 32, 213, 204, 124, 16, 26, 5, 253, 34 }, new byte[] { 194, 99, 184, 97, 57, 59, 248, 71, 252, 6, 66, 180, 13, 185, 174, 158, 0, 190, 54, 245, 75, 221, 82, 202, 226, 176, 71, 20, 222, 191, 33, 97, 206, 144, 79, 208, 217, 211, 139, 7, 68, 193, 254, 155, 32, 53, 43, 227, 82, 232, 197, 132, 71, 12, 248, 151, 215, 218, 211, 194, 142, 148, 19, 72 }, new Guid("a06310b2-5b34-4760-8b1b-33ec48dd3cfa"), "Active", "System", "SYSTEM", "SystemUser" });
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
                name: "TENANTS",
                schema: "DBO");

            migrationBuilder.DropTable(
                name: "USERS",
                schema: "DBO");
        }
    }
}
