using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class added_system_admin_role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("8ed741fe-e8e5-4524-814c-3fa8e9dd8654"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("94d1676f-c45b-4557-b018-0e30332250b0"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("37303b9a-000e-4d5c-b2b7-c3a3bbc1f89d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("f4e77536-5444-4698-9ebb-e81fcbfe738b"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("758b38d9-e1eb-42bd-b3a3-3418bb7f21e0"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("b14ea420-3499-46a6-9cc8-ded91ca0d0c6"));

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("665a3cee-2838-4c07-9621-407ce31adea3"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("e0eef1a0-c04e-45dc-857a-bb5108f801c4"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("026bf3ff-4352-4d46-9594-37bf45a4e1ae"), "User", 1 },
                    { new Guid("11f15747-a854-48a9-89d0-4f7b2343a217"), "System Admin", 1 },
                    { new Guid("13fc0a47-5970-453a-8d90-396ae58fd201"), "Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("4d5819a3-6faa-4133-81ac-b06a9d5268a4"), new Guid("665a3cee-2838-4c07-9621-407ce31adea3"), "admin123", new Guid("e0eef1a0-c04e-45dc-857a-bb5108f801c4"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("658f6054-d0bc-4e3b-9a24-65774e91fca2"), new Guid("11f15747-a854-48a9-89d0-4f7b2343a217"), null, 0, new Guid("4d5819a3-6faa-4133-81ac-b06a9d5268a4") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("026bf3ff-4352-4d46-9594-37bf45a4e1ae"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("13fc0a47-5970-453a-8d90-396ae58fd201"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("658f6054-d0bc-4e3b-9a24-65774e91fca2"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("11f15747-a854-48a9-89d0-4f7b2343a217"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("4d5819a3-6faa-4133-81ac-b06a9d5268a4"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("665a3cee-2838-4c07-9621-407ce31adea3"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("e0eef1a0-c04e-45dc-857a-bb5108f801c4"));

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("758b38d9-e1eb-42bd-b3a3-3418bb7f21e0"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("b14ea420-3499-46a6-9cc8-ded91ca0d0c6"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("37303b9a-000e-4d5c-b2b7-c3a3bbc1f89d"), "Admin", 1 },
                    { new Guid("8ed741fe-e8e5-4524-814c-3fa8e9dd8654"), "User", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("f4e77536-5444-4698-9ebb-e81fcbfe738b"), new Guid("758b38d9-e1eb-42bd-b3a3-3418bb7f21e0"), "admin123", new Guid("b14ea420-3499-46a6-9cc8-ded91ca0d0c6"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("94d1676f-c45b-4557-b018-0e30332250b0"), new Guid("37303b9a-000e-4d5c-b2b7-c3a3bbc1f89d"), null, 0, new Guid("f4e77536-5444-4698-9ebb-e81fcbfe738b") });
        }
    }
}
