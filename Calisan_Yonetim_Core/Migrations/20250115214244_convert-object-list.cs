using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class convertobjectlist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "RolePageId",
                table: "Role",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RolePageId",
                table: "Pages",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Role_RolePageId",
                table: "Role",
                column: "RolePageId");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_RolePageId",
                table: "Pages",
                column: "RolePageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_RolePages_RolePageId",
                table: "Pages",
                column: "RolePageId",
                principalTable: "RolePages",
                principalColumn: "RolePageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Role_RolePages_RolePageId",
                table: "Role",
                column: "RolePageId",
                principalTable: "RolePages",
                principalColumn: "RolePageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_RolePages_RolePageId",
                table: "Pages");

            migrationBuilder.DropForeignKey(
                name: "FK_Role_RolePages_RolePageId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Role_RolePageId",
                table: "Role");

            migrationBuilder.DropIndex(
                name: "IX_Pages_RolePageId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "RolePageId",
                table: "Role");

            migrationBuilder.DropColumn(
                name: "RolePageId",
                table: "Pages");

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
    }
}
