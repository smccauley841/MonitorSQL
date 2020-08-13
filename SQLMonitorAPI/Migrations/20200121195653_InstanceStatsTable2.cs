using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class InstanceStatsTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "PLE",
                table: "InstanceStats",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PLE",
                table: "InstanceStats",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
