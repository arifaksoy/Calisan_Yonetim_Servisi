using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class added_default_data_check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("13ca689f-7d8b-45e9-95d7-b942992aa8f2"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("bc32ddd6-3c27-49a4-b8bb-3d7e31516c72"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("94c3cc3e-2f4f-4737-a6ee-957cd1041125"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("9e187bab-743a-43f5-a7ee-7c0dbca3984c"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("e0162d14-9dc9-4ce8-a1f5-e6eaed2b411f"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("bd07b955-61b4-4850-8db2-54fbffabb0a1"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("e1fd8f1d-18c7-45c0-95a0-cdf0d4fd93c1"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
