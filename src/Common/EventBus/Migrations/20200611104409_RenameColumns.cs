using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Common.EventBus.Migrations
{
    public partial class RenameColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "creation_time",
                table: "integration_event_logs");

            migrationBuilder.AddColumn<DateTime>(
                name: "time_stamp",
                table: "integration_event_logs",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "time_stamp",
                table: "integration_event_logs");

            migrationBuilder.AddColumn<DateTime>(
                name: "creation_time",
                table: "integration_event_logs",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
