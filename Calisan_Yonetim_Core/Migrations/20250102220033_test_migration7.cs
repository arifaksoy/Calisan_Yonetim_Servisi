using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class test_migration7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("270e6300-5121-46f9-8d26-0c676d3b0f6f"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("f51b8734-c53f-4c2a-8dd6-92b2aa59c758"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("26739f4a-a209-42fc-920c-c9872648e88f"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("e94ca036-b81c-4154-831b-ab3be5f5ebe6"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("c38cb47e-dc88-44ad-bd5b-1fa3ceaa80d4"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("17a49576-e2e8-46d8-8ba0-04846661fb9f"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("e4362357-afd1-4b25-9e59-f852aa341e34"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("84c503fb-aaea-4ab0-b799-7d35bcfaa6a1"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d6b27534-363d-4f1a-80a9-e9f5961981d4"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("710a9a88-d8d3-4cf1-a4a2-f82aa96ab2e8"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("4c9a19f0-67ea-46a3-9570-a5b56cfc0856"));

            migrationBuilder.CreateTable(
                name: "Pages2",
                columns: table => new
                {
                    PageId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages2", x => x.PageId2);
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("52f452b1-5018-4aa2-8402-a9ef5735d7c2"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "PageDescription", "PageName", "Status" },
                values: new object[,]
                {
                    { new Guid("3a3350f8-781c-4671-80bb-bc8189cca203"), "", "Pages", 1 },
                    { new Guid("e0f21da9-bf09-4724-b7a4-db6e55b61c80"), "", "Company", 1 }
                });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("4c03a9f9-c3c4-46c0-a944-6537ff6c1195"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("80fd5f51-b495-4ed0-9bba-8af1fbd23372"), "Admin", 1 },
                    { new Guid("83e8d543-4230-4486-9071-34bead58ef65"), "User", 1 },
                    { new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"), "System Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("100ab2a1-f004-40f0-8e84-ff5e8f503ddd"), new Guid("3a3350f8-781c-4671-80bb-bc8189cca203"), new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"), 1 });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("aba70ed3-7ce8-4b4d-9b65-6e5f40a70ac8"), new Guid("e0f21da9-bf09-4724-b7a4-db6e55b61c80"), new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("389a0451-994c-46e1-bb04-041c6bc90c44"), new Guid("52f452b1-5018-4aa2-8402-a9ef5735d7c2"), "admin123", new Guid("4c03a9f9-c3c4-46c0-a944-6537ff6c1195"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("250e8c6f-d742-41a4-ac87-b6c9a66c84d0"), new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"), null, 1, new Guid("389a0451-994c-46e1-bb04-041c6bc90c44") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pages2");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("80fd5f51-b495-4ed0-9bba-8af1fbd23372"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("83e8d543-4230-4486-9071-34bead58ef65"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("100ab2a1-f004-40f0-8e84-ff5e8f503ddd"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("aba70ed3-7ce8-4b4d-9b65-6e5f40a70ac8"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("250e8c6f-d742-41a4-ac87-b6c9a66c84d0"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("3a3350f8-781c-4671-80bb-bc8189cca203"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("e0f21da9-bf09-4724-b7a4-db6e55b61c80"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("389a0451-994c-46e1-bb04-041c6bc90c44"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("52f452b1-5018-4aa2-8402-a9ef5735d7c2"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("4c03a9f9-c3c4-46c0-a944-6537ff6c1195"));

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("710a9a88-d8d3-4cf1-a4a2-f82aa96ab2e8"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "PageDescription", "PageName", "Status" },
                values: new object[,]
                {
                    { new Guid("17a49576-e2e8-46d8-8ba0-04846661fb9f"), "", "Pages", 1 },
                    { new Guid("e4362357-afd1-4b25-9e59-f852aa341e34"), "", "Company", 1 }
                });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("4c9a19f0-67ea-46a3-9570-a5b56cfc0856"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("270e6300-5121-46f9-8d26-0c676d3b0f6f"), "Admin", 1 },
                    { new Guid("84c503fb-aaea-4ab0-b799-7d35bcfaa6a1"), "System Admin", 1 },
                    { new Guid("f51b8734-c53f-4c2a-8dd6-92b2aa59c758"), "User", 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("26739f4a-a209-42fc-920c-c9872648e88f"), new Guid("e4362357-afd1-4b25-9e59-f852aa341e34"), new Guid("84c503fb-aaea-4ab0-b799-7d35bcfaa6a1"), 1 });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("e94ca036-b81c-4154-831b-ab3be5f5ebe6"), new Guid("17a49576-e2e8-46d8-8ba0-04846661fb9f"), new Guid("84c503fb-aaea-4ab0-b799-7d35bcfaa6a1"), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("d6b27534-363d-4f1a-80a9-e9f5961981d4"), new Guid("710a9a88-d8d3-4cf1-a4a2-f82aa96ab2e8"), "admin123", new Guid("4c9a19f0-67ea-46a3-9570-a5b56cfc0856"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("c38cb47e-dc88-44ad-bd5b-1fa3ceaa80d4"), new Guid("84c503fb-aaea-4ab0-b799-7d35bcfaa6a1"), null, 1, new Guid("d6b27534-363d-4f1a-80a9-e9f5961981d4") });
        }
    }
}
