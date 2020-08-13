using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class ForeignKeyFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_BackupHistory_SQLInstances_InstanceIdID",
            //    table: "BackupHistory");
            //
            //migrationBuilder.DropForeignKey(
            //    name: "FK_SQLDatabases_SQLInstances_InstanceIdID",
            //    table: "SQLDatabases");

            migrationBuilder.DropIndex(
                name: "IX_SQLDatabases_InstanceIdID",
                table: "SQLDatabases");

            migrationBuilder.DropIndex(
                name: "IX_BackupHistory_InstanceIdID",
                table: "BackupHistory");

            migrationBuilder.DropColumn(
                name: "InstanceIdID",
                table: "SQLDatabases");

            migrationBuilder.DropColumn(
                name: "InstanceIdID",
                table: "BackupHistory");

            migrationBuilder.AddColumn<int>(
                name: "InstanceID",
                table: "SQLDatabases",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstanceID",
                table: "BackupHistory",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SQLDatabases_InstanceID",
                table: "SQLDatabases",
                column: "InstanceID");

            migrationBuilder.CreateIndex(
                name: "IX_BackupHistory_InstanceID",
                table: "BackupHistory",
                column: "InstanceID");

            migrationBuilder.AddForeignKey(
                name: "FK_BackupHistory_SQLInstances_InstanceID",
                table: "BackupHistory",
                column: "InstanceID",
                principalTable: "SQLInstances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SQLDatabases_SQLInstances_InstanceID",
                table: "SQLDatabases",
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

            migrationBuilder.DropForeignKey(
                name: "FK_SQLDatabases_SQLInstances_InstanceID",
                table: "SQLDatabases");

            migrationBuilder.DropIndex(
                name: "IX_SQLDatabases_InstanceID",
                table: "SQLDatabases");

            migrationBuilder.DropIndex(
                name: "IX_BackupHistory_InstanceID",
                table: "BackupHistory");

            migrationBuilder.DropColumn(
                name: "InstanceID",
                table: "SQLDatabases");

            migrationBuilder.DropColumn(
                name: "InstanceID",
                table: "BackupHistory");

            migrationBuilder.AddColumn<int>(
                name: "InstanceIdID",
                table: "SQLDatabases",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "InstanceIdID",
                table: "BackupHistory",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SQLDatabases_InstanceIdID",
                table: "SQLDatabases",
                column: "InstanceIdID");

            migrationBuilder.CreateIndex(
                name: "IX_BackupHistory_InstanceIdID",
                table: "BackupHistory",
                column: "InstanceIdID");

            migrationBuilder.AddForeignKey(
                name: "FK_BackupHistory_SQLInstances_InstanceIdID",
                table: "BackupHistory",
                column: "InstanceIdID",
                principalTable: "SQLInstances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SQLDatabases_SQLInstances_InstanceIdID",
                table: "SQLDatabases",
                column: "InstanceIdID",
                principalTable: "SQLInstances",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
