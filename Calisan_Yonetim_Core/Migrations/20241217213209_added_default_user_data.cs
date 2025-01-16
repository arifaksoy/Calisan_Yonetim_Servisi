using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class added_default_user_data : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("5b822d3a-e630-464b-bd77-d2865df5ecef"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("e44e37e5-c155-4771-bd82-d66186e0f267"));

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Role",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[] { new Guid("5b822d3a-e630-464b-bd77-d2865df5ecef"), "User", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[] { new Guid("e44e37e5-c155-4771-bd82-d66186e0f267"), "Admin", 1 });
        }
    }
}
