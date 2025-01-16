using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class test_migration5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("68b7d150-7352-478b-8cbf-ab637f74e87e"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("fde4e411-defb-4d87-9488-233d636d9381"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("a236a099-4fe5-4cfb-8b2b-d40136de1572"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("afbb1847-4ffc-40fc-874a-cb55a91f6e60"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("40c3683d-f63d-42e3-8aeb-846e01fee842"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("cb5ef583-350e-4ab3-a806-1651383ad863"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("deb9425f-7053-4b15-b636-692b833b0432"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("eddbcdf4-0357-4ca5-b34a-de52cd8eebe6"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("cc04b54d-14a6-42d6-a011-c806d7fd7749"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("92a26aad-8cbe-4712-a1a0-2a7c75a26fa6"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("8a8e95e9-88d5-40b9-8495-e41c13f33cd1"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("92a26aad-8cbe-4712-a1a0-2a7c75a26fa6"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "PageDescription", "PageName", "Status" },
                values: new object[,]
                {
                    { new Guid("cb5ef583-350e-4ab3-a806-1651383ad863"), "", "Pages", 1 },
                    { new Guid("deb9425f-7053-4b15-b636-692b833b0432"), "", "Company", 1 }
                });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("8a8e95e9-88d5-40b9-8495-e41c13f33cd1"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("68b7d150-7352-478b-8cbf-ab637f74e87e"), "Admin", 1 },
                    { new Guid("eddbcdf4-0357-4ca5-b34a-de52cd8eebe6"), "System Admin", 1 },
                    { new Guid("fde4e411-defb-4d87-9488-233d636d9381"), "User", 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("a236a099-4fe5-4cfb-8b2b-d40136de1572"), new Guid("cb5ef583-350e-4ab3-a806-1651383ad863"), new Guid("eddbcdf4-0357-4ca5-b34a-de52cd8eebe6"), 1 });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("afbb1847-4ffc-40fc-874a-cb55a91f6e60"), new Guid("deb9425f-7053-4b15-b636-692b833b0432"), new Guid("eddbcdf4-0357-4ca5-b34a-de52cd8eebe6"), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("cc04b54d-14a6-42d6-a011-c806d7fd7749"), new Guid("92a26aad-8cbe-4712-a1a0-2a7c75a26fa6"), "admin123", new Guid("8a8e95e9-88d5-40b9-8495-e41c13f33cd1"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("40c3683d-f63d-42e3-8aeb-846e01fee842"), new Guid("eddbcdf4-0357-4ca5-b34a-de52cd8eebe6"), null, 1, new Guid("cc04b54d-14a6-42d6-a011-c806d7fd7749") });
        }
    }
}
