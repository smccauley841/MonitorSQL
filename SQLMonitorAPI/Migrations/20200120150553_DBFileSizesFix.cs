using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class DBFileSizesFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "TotalLogUsedBytes",
                table: "DatabaseFileSize",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "TotalLogSizeBytes",
                table: "DatabaseFileSize",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "TotalDataUsedBytes",
                table: "DatabaseFileSize",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<long>(
                name: "TotalDataSizeBytes",
                table: "DatabaseFileSize",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "TotalLogUsedBytes",
                table: "DatabaseFileSize",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "TotalLogSizeBytes",
                table: "DatabaseFileSize",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "TotalDataUsedBytes",
                table: "DatabaseFileSize",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<int>(
                name: "TotalDataSizeBytes",
                table: "DatabaseFileSize",
                type: "int",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
