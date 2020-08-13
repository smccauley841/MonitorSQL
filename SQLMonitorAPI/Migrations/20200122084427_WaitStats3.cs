using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class WaitStats3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "WaitPercentage",
                table: "InstanceWaitStats",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "WaitPercentage",
                table: "InstanceWaitStats",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));
        }
    }
}
