using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class test_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("43aa2a14-0916-42f6-885f-fe2c6df38654"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("a5fae54f-2362-4588-8195-5a725a3287f3"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("5496cca8-cd98-48ad-b599-ce585db78955"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("d8fe3d2c-a0b2-44ba-8db5-afcf00a5bd2e"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("9651a505-d2fe-46fd-a15e-446a455c02cc"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("bd53a170-d37d-431c-a6b7-7f11631766cf"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("c31d3b5b-b61b-45ce-ade9-5f3b6280e0ec"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("d87af8c8-d833-464a-8d19-b7f3dc08ab02"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("d3cf7d21-1b7d-49a2-b157-557de0db4a3a"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("585f7be4-7270-407a-81a8-5099a555bbc9"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("628b85f0-453d-46ad-9939-fbfc5714f25e"));

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("9b53910d-57a5-4ddd-8014-650a4197bc7b"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "PageDescription", "PageName", "Status" },
                values: new object[,]
                {
                    { new Guid("77622a6d-059d-4deb-aed8-fbe079f5fae4"), "", "Company", 1 },
                    { new Guid("d542ef98-cebc-43ab-8d34-1e2d87b14dfa"), "", "Pages", 1 }
                });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("091f3504-6eea-453a-b707-6143b70f4b53"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("17c3b988-6331-474c-bab9-c3bd6421fae2"), "User", 1 },
                    { new Guid("91ee1bc9-c4a5-4d63-9ca3-638af99b430c"), "System Admin", 1 },
                    { new Guid("ea5c3c5d-d42e-4cec-bbfb-e95faefe90a3"), "Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("028e16d5-5be0-4d8f-83c4-56b780f020d9"), new Guid("d542ef98-cebc-43ab-8d34-1e2d87b14dfa"), new Guid("91ee1bc9-c4a5-4d63-9ca3-638af99b430c"), 1 });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("d9e38d5a-30b6-4f82-add0-2dee12c6b72b"), new Guid("77622a6d-059d-4deb-aed8-fbe079f5fae4"), new Guid("91ee1bc9-c4a5-4d63-9ca3-638af99b430c"), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("e2e424b8-e092-442a-9a8c-9851152c7016"), new Guid("9b53910d-57a5-4ddd-8014-650a4197bc7b"), "admin123", new Guid("091f3504-6eea-453a-b707-6143b70f4b53"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("7271d224-8e73-4f03-86f7-82f4f932bbeb"), new Guid("91ee1bc9-c4a5-4d63-9ca3-638af99b430c"), null, 1, new Guid("e2e424b8-e092-442a-9a8c-9851152c7016") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("17c3b988-6331-474c-bab9-c3bd6421fae2"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("ea5c3c5d-d42e-4cec-bbfb-e95faefe90a3"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("028e16d5-5be0-4d8f-83c4-56b780f020d9"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("d9e38d5a-30b6-4f82-add0-2dee12c6b72b"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("7271d224-8e73-4f03-86f7-82f4f932bbeb"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("77622a6d-059d-4deb-aed8-fbe079f5fae4"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("d542ef98-cebc-43ab-8d34-1e2d87b14dfa"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("91ee1bc9-c4a5-4d63-9ca3-638af99b430c"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("e2e424b8-e092-442a-9a8c-9851152c7016"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("9b53910d-57a5-4ddd-8014-650a4197bc7b"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("091f3504-6eea-453a-b707-6143b70f4b53"));

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("585f7be4-7270-407a-81a8-5099a555bbc9"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "PageDescription", "PageName", "Status" },
                values: new object[,]
                {
                    { new Guid("bd53a170-d37d-431c-a6b7-7f11631766cf"), "", "Pages", 1 },
                    { new Guid("c31d3b5b-b61b-45ce-ade9-5f3b6280e0ec"), "", "Company", 1 }
                });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("628b85f0-453d-46ad-9939-fbfc5714f25e"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("43aa2a14-0916-42f6-885f-fe2c6df38654"), "Admin", 1 },
                    { new Guid("a5fae54f-2362-4588-8195-5a725a3287f3"), "User", 1 },
                    { new Guid("d87af8c8-d833-464a-8d19-b7f3dc08ab02"), "System Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("5496cca8-cd98-48ad-b599-ce585db78955"), new Guid("bd53a170-d37d-431c-a6b7-7f11631766cf"), new Guid("d87af8c8-d833-464a-8d19-b7f3dc08ab02"), 1 });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("d8fe3d2c-a0b2-44ba-8db5-afcf00a5bd2e"), new Guid("c31d3b5b-b61b-45ce-ade9-5f3b6280e0ec"), new Guid("d87af8c8-d833-464a-8d19-b7f3dc08ab02"), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("d3cf7d21-1b7d-49a2-b157-557de0db4a3a"), new Guid("585f7be4-7270-407a-81a8-5099a555bbc9"), "admin123", new Guid("628b85f0-453d-46ad-9939-fbfc5714f25e"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("9651a505-d2fe-46fd-a15e-446a455c02cc"), new Guid("d87af8c8-d833-464a-8d19-b7f3dc08ab02"), null, 1, new Guid("d3cf7d21-1b7d-49a2-b157-557de0db4a3a") });
        }
    }
}
