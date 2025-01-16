using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class Added_user_management_tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Personnel_PersonnelId",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "PersonnelId",
                table: "UserRole",
                newName: "RoleId1");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_PersonnelId",
                table: "UserRole",
                newName: "IX_UserRole_RoleId1");

            migrationBuilder.AlterColumn<byte>(
                name: "Password",
                table: "Users",
                type: "tinyint",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Role",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Role_RoleId1",
                table: "UserRole",
                column: "RoleId1",
                principalTable: "Role",
                principalColumn: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRole_Role_RoleId1",
                table: "UserRole");

            migrationBuilder.RenameColumn(
                name: "RoleId1",
                table: "UserRole",
                newName: "PersonnelId");

            migrationBuilder.RenameIndex(
                name: "IX_UserRole_RoleId1",
                table: "UserRole",
                newName: "IX_UserRole_PersonnelId");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(byte),
                oldType: "tinyint");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRole_Personnel_PersonnelId",
                table: "UserRole",
                column: "PersonnelId",
                principalTable: "Personnel",
                principalColumn: "PersonnelId");
        }
    }
}
