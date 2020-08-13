using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class InstanceStatsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstanceStats",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PLE = table.Column<int>(nullable: false),
                    Timestamp = table.Column<DateTime>(nullable: false),
                    InstanceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceStats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstanceStats_SQLInstances_InstanceID",
                        column: x => x.InstanceID,
                        principalTable: "SQLInstances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstanceStats_InstanceID",
                table: "InstanceStats",
                column: "InstanceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstanceStats");
        }
    }
}
