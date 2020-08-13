using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SQLMonitorAPI.Migrations
{
    public partial class UpdatedColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IsAlwaysOn",
                table: "SQLInstances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IsClustered",
                table: "SQLInstances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NoOfDatabases",
                table: "SQLInstances",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OSName",
                table: "SQLInstances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhysicalServerName",
                table: "SQLInstances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SQLCollation",
                table: "SQLInstances",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SQLCreateDate",
                table: "SQLInstances",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SQLEdition",
                table: "SQLInstances",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SQLVersion",
                table: "SQLInstances",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAlwaysOn",
                table: "SQLInstances");

            migrationBuilder.DropColumn(
                name: "IsClustered",
                table: "SQLInstances");

            migrationBuilder.DropColumn(
                name: "NoOfDatabases",
                table: "SQLInstances");

            migrationBuilder.DropColumn(
                name: "OSName",
                table: "SQLInstances");

            migrationBuilder.DropColumn(
                name: "PhysicalServerName",
                table: "SQLInstances");

            migrationBuilder.DropColumn(
                name: "SQLCollation",
                table: "SQLInstances");

            migrationBuilder.DropColumn(
                name: "SQLCreateDate",
                table: "SQLInstances");

            migrationBuilder.DropColumn(
                name: "SQLEdition",
                table: "SQLInstances");

            migrationBuilder.DropColumn(
                name: "SQLVersion",
                table: "SQLInstances");
        }
    }
}
