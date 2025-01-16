using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class added_page_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("2e62dab5-4a7b-4167-b111-2e177eddc43f"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("389da82f-3bf6-4ce2-8eb4-2ac4115ec6f9"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("80bfb29c-6740-4372-a3f7-12d275da7e2d"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("53fa1465-665e-4f06-acac-0e136bc0856b"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0f4eb24c-2832-4b1a-8e46-fc534d8a5032"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("d1b3b3ab-89a0-480c-b64d-f86b50452f6d"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("0e3996f6-c720-468d-bc14-3cac6e7bbdbc"));

            migrationBuilder.CreateTable(
                name: "Pages",
                columns: table => new
                {
                    PageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages", x => x.PageId);
                });

            migrationBuilder.CreateTable(
                name: "RolePages",
                columns: table => new
                {
                    RolePageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePages", x => x.RolePageId);
                    table.ForeignKey(
                        name: "FK_RolePages_Pages_PageId",
                        column: x => x.PageId,
                        principalTable: "Pages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePages_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Role",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("58c2f602-afa9-4093-a9b2-49436cd34707"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "PageDescription", "PageName", "Status" },
                values: new object[,]
                {
                    { new Guid("bd07b955-61b4-4850-8db2-54fbffabb0a1"), "", "Pages", 1 },
                    { new Guid("e1fd8f1d-18c7-45c0-95a0-cdf0d4fd93c1"), "", "Company", 1 }
                });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("340385ba-7e11-484d-b4ab-74b6af7f4cce"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("13ca689f-7d8b-45e9-95d7-b942992aa8f2"), "Admin", 1 },
                    { new Guid("bc32ddd6-3c27-49a4-b8bb-3d7e31516c72"), "User", 1 },
                    { new Guid("e2fe482c-1a0d-4e56-bc67-29f2d732e72f"), "System Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("94c3cc3e-2f4f-4737-a6ee-957cd1041125"), new Guid("bd07b955-61b4-4850-8db2-54fbffabb0a1"), new Guid("e2fe482c-1a0d-4e56-bc67-29f2d732e72f"), 1 });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("9e187bab-743a-43f5-a7ee-7c0dbca3984c"), new Guid("e1fd8f1d-18c7-45c0-95a0-cdf0d4fd93c1"), new Guid("e2fe482c-1a0d-4e56-bc67-29f2d732e72f"), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("e141280d-f6c4-4e6b-b9a1-c49761d6f9ce"), new Guid("58c2f602-afa9-4093-a9b2-49436cd34707"), "admin123", new Guid("340385ba-7e11-484d-b4ab-74b6af7f4cce"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("e0162d14-9dc9-4ce8-a1f5-e6eaed2b411f"), new Guid("e2fe482c-1a0d-4e56-bc67-29f2d732e72f"), null, 1, new Guid("e141280d-f6c4-4e6b-b9a1-c49761d6f9ce") });

            migrationBuilder.CreateIndex(
                name: "IX_RolePages_PageId",
                table: "RolePages",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePages_RoleId",
                table: "RolePages",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolePages");

            migrationBuilder.DropTable(
                name: "Pages");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("13ca689f-7d8b-45e9-95d7-b942992aa8f2"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("bc32ddd6-3c27-49a4-b8bb-3d7e31516c72"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("e0162d14-9dc9-4ce8-a1f5-e6eaed2b411f"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("e2fe482c-1a0d-4e56-bc67-29f2d732e72f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e141280d-f6c4-4e6b-b9a1-c49761d6f9ce"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("58c2f602-afa9-4093-a9b2-49436cd34707"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("340385ba-7e11-484d-b4ab-74b6af7f4cce"));

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("d1b3b3ab-89a0-480c-b64d-f86b50452f6d"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("0e3996f6-c720-468d-bc14-3cac6e7bbdbc"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("2e62dab5-4a7b-4167-b111-2e177eddc43f"), "Admin", 1 },
                    { new Guid("389da82f-3bf6-4ce2-8eb4-2ac4115ec6f9"), "User", 1 },
                    { new Guid("53fa1465-665e-4f06-acac-0e136bc0856b"), "System Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("0f4eb24c-2832-4b1a-8e46-fc534d8a5032"), new Guid("d1b3b3ab-89a0-480c-b64d-f86b50452f6d"), "admin123", new Guid("0e3996f6-c720-468d-bc14-3cac6e7bbdbc"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("80bfb29c-6740-4372-a3f7-12d275da7e2d"), new Guid("53fa1465-665e-4f06-acac-0e136bc0856b"), null, 1, new Guid("0f4eb24c-2832-4b1a-8e46-fc534d8a5032") });
        }
    }
}
