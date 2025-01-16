using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class updated_User_role_model2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("897ca236-4580-47a0-87a5-7de382cf894c"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("b72de24e-51f7-4321-8b65-76c5c01158d8"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("daca23be-d406-490f-8cad-667f217aa975"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("fd7fa1f3-0469-4226-a256-989015a60abf"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("be7a0f69-675c-4342-ad9f-80106baa0398"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("f146dadd-2aa6-45fe-a50d-e106565f4f75"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("2adf186b-ca87-468d-b3f4-5a8d59dece67"));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("f146dadd-2aa6-45fe-a50d-e106565f4f75"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("2adf186b-ca87-468d-b3f4-5a8d59dece67"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("897ca236-4580-47a0-87a5-7de382cf894c"), "User", 1 },
                    { new Guid("b72de24e-51f7-4321-8b65-76c5c01158d8"), "Admin", 1 },
                    { new Guid("fd7fa1f3-0469-4226-a256-989015a60abf"), "System Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("be7a0f69-675c-4342-ad9f-80106baa0398"), new Guid("f146dadd-2aa6-45fe-a50d-e106565f4f75"), "admin123", new Guid("2adf186b-ca87-468d-b3f4-5a8d59dece67"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("daca23be-d406-490f-8cad-667f217aa975"), new Guid("fd7fa1f3-0469-4226-a256-989015a60abf"), null, 0, new Guid("be7a0f69-675c-4342-ad9f-80106baa0398") });
        }
    }
}
