using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class updated_user_model_password_property_type : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("bf50c8f4-1376-4b29-843d-e23f3e9e3cc6"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("efe839cf-6398-40a2-be22-186f8bbf5720"));

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[] { new Guid("8153bc1e-5ac3-4586-a0cf-f155039c15d2"), "Admin", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[] { new Guid("dbdd5c8d-1183-4e6e-906e-d9dbd463c5d1"), "User", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("8153bc1e-5ac3-4586-a0cf-f155039c15d2"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("dbdd5c8d-1183-4e6e-906e-d9dbd463c5d1"));

            migrationBuilder.AlterColumn<byte>(
                name: "Password",
                table: "Users",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[] { new Guid("bf50c8f4-1376-4b29-843d-e23f3e9e3cc6"), "Admin", 0 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[] { new Guid("efe839cf-6398-40a2-be22-186f8bbf5720"), "User", 0 });
        }
    }
}
