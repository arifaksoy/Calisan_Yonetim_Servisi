using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class test_migration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
