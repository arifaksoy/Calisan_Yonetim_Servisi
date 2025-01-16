using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class addforeignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RolePages_PageId",
                table: "RolePages",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_RolePages_RoleId",
                table: "RolePages",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePages_Pages_PageId",
                table: "RolePages",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePages_Role_RoleId",
                table: "RolePages",
                column: "RoleId",
                principalTable: "Role",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RolePages_Pages_PageId",
                table: "RolePages");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePages_Role_RoleId",
                table: "RolePages");

            migrationBuilder.DropIndex(
                name: "IX_RolePages_PageId",
                table: "RolePages");

            migrationBuilder.DropIndex(
                name: "IX_RolePages_RoleId",
                table: "RolePages");
        }
    }
}
