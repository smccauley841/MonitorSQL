using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class ServerStatsFix2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServerStat",
                table: "ServerStats");

            migrationBuilder.DropColumn(
                name: "Value",
                table: "ServerStats");

            migrationBuilder.AddColumn<double>(
                name: "CPU",
                table: "ServerStats",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "RAM",
                table: "ServerStats",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CPU",
                table: "ServerStats");

            migrationBuilder.DropColumn(
                name: "RAM",
                table: "ServerStats");

            migrationBuilder.AddColumn<string>(
                name: "ServerStat",
                table: "ServerStats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "ServerStats",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
