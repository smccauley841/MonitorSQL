using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class ForeignKeyFixes2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackupHistory_SQLInstances_InstanceID",
                table: "BackupHistory");

            migrationBuilder.AlterColumn<int>(
                name: "InstanceID",
                table: "BackupHistory",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Database",
                table: "BackupHistory",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BackupHistory_SQLInstances_InstanceID",
                table: "BackupHistory",
                column: "InstanceID",
                principalTable: "SQLInstances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackupHistory_SQLInstances_InstanceID",
                table: "BackupHistory");

            migrationBuilder.AlterColumn<int>(
                name: "InstanceID",
                table: "BackupHistory",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Database",
                table: "BackupHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_BackupHistory_SQLInstances_InstanceID",
                table: "BackupHistory",
                column: "InstanceID",
                principalTable: "SQLInstances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
