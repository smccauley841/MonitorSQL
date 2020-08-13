using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class BackupTable2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackupHistory_SQLInstances_InstanceIdID",
                table: "BackupHistory");

            migrationBuilder.AlterColumn<int>(
                name: "InstanceIdID",
                table: "BackupHistory",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BackupType",
                table: "BackupHistory",
                maxLength: 1,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_BackupHistory_SQLInstances_InstanceIdID",
                table: "BackupHistory",
                column: "InstanceIdID",
                principalTable: "SQLInstances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BackupHistory_SQLInstances_InstanceIdID",
                table: "BackupHistory");

            migrationBuilder.DropColumn(
                name: "BackupType",
                table: "BackupHistory");

            migrationBuilder.AlterColumn<int>(
                name: "InstanceIdID",
                table: "BackupHistory",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_BackupHistory_SQLInstances_InstanceIdID",
                table: "BackupHistory",
                column: "InstanceIdID",
                principalTable: "SQLInstances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
