using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class updated_User_role_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
