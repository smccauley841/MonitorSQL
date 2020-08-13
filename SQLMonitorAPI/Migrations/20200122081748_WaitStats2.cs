using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class WaitStats2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstanceWaitStats",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WaitType = table.Column<string>(nullable: true),
                    WaitPercentage = table.Column<double>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    InstanceID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstanceWaitStats", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InstanceWaitStats_SQLInstances_InstanceID",
                        column: x => x.InstanceID,
                        principalTable: "SQLInstances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstanceWaitStats_InstanceID",
                table: "InstanceWaitStats",
                column: "InstanceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstanceWaitStats");
        }
    }
}
