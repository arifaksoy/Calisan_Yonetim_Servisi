using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class addedconstantdefaultdata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Pages2");

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("80fd5f51-b495-4ed0-9bba-8af1fbd23372"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("83e8d543-4230-4486-9071-34bead58ef65"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("100ab2a1-f004-40f0-8e84-ff5e8f503ddd"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("aba70ed3-7ce8-4b4d-9b65-6e5f40a70ac8"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("250e8c6f-d742-41a4-ac87-b6c9a66c84d0"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("3a3350f8-781c-4671-80bb-bc8189cca203"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("e0f21da9-bf09-4724-b7a4-db6e55b61c80"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("389a0451-994c-46e1-bb04-041c6bc90c44"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("52f452b1-5018-4aa2-8402-a9ef5735d7c2"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("4c03a9f9-c3c4-46c0-a944-6537ff6c1195"));

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("71fcdef1-d547-49db-8a4d-c441e959c0f4"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "PageDescription", "PageName", "Status" },
                values: new object[,]
                {
                    { new Guid("71fcdef5-d547-49db-8a4d-c441e959c0f4"), "", "Company", 1 },
                    { new Guid("71fcdef6-d547-49db-8a4d-c441e959c0f4"), "", "Pages", 1 }
                });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("71fcdef2-d547-49db-8a4d-c441e959c0f4"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("71fcdef4-d547-49db-8a4d-c441e959c0f4"), "System Admin", 1 },
                    { new Guid("71fcdef7-d547-49db-8a4d-c441e959c0f4"), "Admin", 1 },
                    { new Guid("71fcdef8-d547-49db-8a4d-c441e959c0f4"), "User", 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("71fcdefa-d547-49db-8a4d-c441e959c0f4"), new Guid("71fcdef5-d547-49db-8a4d-c441e959c0f4"), new Guid("71fcdef4-d547-49db-8a4d-c441e959c0f4"), 1 });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("71fcdefb-d547-49db-8a4d-c441e959c0f4"), new Guid("71fcdef6-d547-49db-8a4d-c441e959c0f4"), new Guid("71fcdef4-d547-49db-8a4d-c441e959c0f4"), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("71fcdef3-d547-49db-8a4d-c441e959c0f4"), new Guid("71fcdef1-d547-49db-8a4d-c441e959c0f4"), "admin123", new Guid("71fcdef2-d547-49db-8a4d-c441e959c0f4"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("71fcdef9-d547-49db-8a4d-c441e959c0f4"), new Guid("71fcdef4-d547-49db-8a4d-c441e959c0f4"), null, 1, new Guid("71fcdef3-d547-49db-8a4d-c441e959c0f4") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("71fcdef7-d547-49db-8a4d-c441e959c0f4"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("71fcdef8-d547-49db-8a4d-c441e959c0f4"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("71fcdefa-d547-49db-8a4d-c441e959c0f4"));

            migrationBuilder.DeleteData(
                table: "RolePages",
                keyColumn: "RolePageId",
                keyValue: new Guid("71fcdefb-d547-49db-8a4d-c441e959c0f4"));

            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "UserRoleId",
                keyValue: new Guid("71fcdef9-d547-49db-8a4d-c441e959c0f4"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("71fcdef5-d547-49db-8a4d-c441e959c0f4"));

            migrationBuilder.DeleteData(
                table: "Pages",
                keyColumn: "PageId",
                keyValue: new Guid("71fcdef6-d547-49db-8a4d-c441e959c0f4"));

            migrationBuilder.DeleteData(
                table: "Role",
                keyColumn: "RoleId",
                keyValue: new Guid("71fcdef4-d547-49db-8a4d-c441e959c0f4"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("71fcdef3-d547-49db-8a4d-c441e959c0f4"));

            migrationBuilder.DeleteData(
                table: "Company",
                keyColumn: "CompanyId",
                keyValue: new Guid("71fcdef1-d547-49db-8a4d-c441e959c0f4"));

            migrationBuilder.DeleteData(
                table: "Personnel",
                keyColumn: "PersonnelId",
                keyValue: new Guid("71fcdef2-d547-49db-8a4d-c441e959c0f4"));

            migrationBuilder.CreateTable(
                name: "Pages2",
                columns: table => new
                {
                    PageId2 = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageDescription2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PageName2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status2 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pages2", x => x.PageId2);
                });

            migrationBuilder.InsertData(
                table: "Company",
                columns: new[] { "CompanyId", "CompanyName", "CompanyStatus" },
                values: new object[] { new Guid("52f452b1-5018-4aa2-8402-a9ef5735d7c2"), "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Pages",
                columns: new[] { "PageId", "PageDescription", "PageName", "Status" },
                values: new object[,]
                {
                    { new Guid("3a3350f8-781c-4671-80bb-bc8189cca203"), "", "Pages", 1 },
                    { new Guid("e0f21da9-bf09-4724-b7a4-db6e55b61c80"), "", "Company", 1 }
                });

            migrationBuilder.InsertData(
                table: "Personnel",
                columns: new[] { "PersonnelId", "Email", "FistName", "LastName", "Status" },
                values: new object[] { new Guid("4c03a9f9-c3c4-46c0-a944-6537ff6c1195"), "admin@pau.net", "Admin", "Pau", 1 });

            migrationBuilder.InsertData(
                table: "Role",
                columns: new[] { "RoleId", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("80fd5f51-b495-4ed0-9bba-8af1fbd23372"), "Admin", 1 },
                    { new Guid("83e8d543-4230-4486-9071-34bead58ef65"), "User", 1 },
                    { new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"), "System Admin", 1 }
                });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("100ab2a1-f004-40f0-8e84-ff5e8f503ddd"), new Guid("3a3350f8-781c-4671-80bb-bc8189cca203"), new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"), 1 });

            migrationBuilder.InsertData(
                table: "RolePages",
                columns: new[] { "RolePageId", "PageId", "RoleId", "Status" },
                values: new object[] { new Guid("aba70ed3-7ce8-4b4d-9b65-6e5f40a70ac8"), new Guid("e0f21da9-bf09-4724-b7a4-db6e55b61c80"), new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"), 1 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CompanyId", "Password", "PersonnelId", "Status", "Username" },
                values: new object[] { new Guid("389a0451-994c-46e1-bb04-041c6bc90c44"), new Guid("52f452b1-5018-4aa2-8402-a9ef5735d7c2"), "admin123", new Guid("4c03a9f9-c3c4-46c0-a944-6537ff6c1195"), 1, "admin" });

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "UserRoleId", "RoleId", "RoleId1", "Status", "UserId" },
                values: new object[] { new Guid("250e8c6f-d742-41a4-ac87-b6c9a66c84d0"), new Guid("b51e746e-5514-428f-81d3-7c23b8fc170d"), null, 1, new Guid("389a0451-994c-46e1-bb04-041c6bc90c44") });
        }
    }
}
