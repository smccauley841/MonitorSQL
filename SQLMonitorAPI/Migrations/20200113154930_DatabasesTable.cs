using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class DatabasesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SQLDatabases",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InstanceIdID = table.Column<int>(nullable: true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Collation = table.Column<string>(maxLength: 50, nullable: false),
                    Status = table.Column<string>(maxLength: 100, nullable: true),
                    RecoveryModel = table.Column<string>(maxLength: 100, nullable: true),
                    Owner = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SQLDatabases", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SQLDatabases_SQLInstances_InstanceIdID",
                        column: x => x.InstanceIdID,
                        principalTable: "SQLInstances",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SQLDatabases_InstanceIdID",
                table: "SQLDatabases",
                column: "InstanceIdID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SQLDatabases");
        }
    }
}
