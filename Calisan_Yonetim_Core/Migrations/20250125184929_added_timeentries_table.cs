using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Calisan_Yonetim_Core.Migrations
{
    public partial class added_timeentries_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeEntries",
                columns: table => new
                {
                    TimeEntriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TimeEntriesDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProjectId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PersonnelId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntries", x => x.TimeEntriesId);
                    table.ForeignKey(
                        name: "FK_TimeEntries_Personnel_PersonnelId",
                        column: x => x.PersonnelId,
                        principalTable: "Personnel",
                        principalColumn: "PersonnelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TimeEntries_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_PersonnelId",
                table: "TimeEntries",
                column: "PersonnelId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_ProjectId",
                table: "TimeEntries",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_TimeEntriesDate_ProjectId_PersonnelId",
                table: "TimeEntries",
                columns: new[] { "TimeEntriesDate", "ProjectId", "PersonnelId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeEntries");
        }
    }
}
