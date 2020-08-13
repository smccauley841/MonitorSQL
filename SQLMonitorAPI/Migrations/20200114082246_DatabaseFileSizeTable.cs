using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class DatabaseFileSizeTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatabaseFileSize",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstanceID = table.Column<int>(nullable: true),
                    DatabaseID = table.Column<int>(nullable: true),
                    TotalDataUsedBytes = table.Column<int>(nullable: false),
                    TotalLogUsedBytes = table.Column<int>(nullable: false),
                    TotalDataSizeBytes = table.Column<int>(nullable: false),
                    TotalLogSizeBytes = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatabaseFileSize", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DatabaseFileSize_SQLDatabases_DatabaseID",
                        column: x => x.DatabaseID,
                        principalTable: "SQLDatabases",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DatabaseFileSize_SQLInstances_InstanceID",
                        column: x => x.InstanceID,
                        principalTable: "SQLInstances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseFileSize_DatabaseID",
                table: "DatabaseFileSize",
                column: "DatabaseID");

            migrationBuilder.CreateIndex(
                name: "IX_DatabaseFileSize_InstanceID",
                table: "DatabaseFileSize",
                column: "InstanceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatabaseFileSize");
        }
    }
}
