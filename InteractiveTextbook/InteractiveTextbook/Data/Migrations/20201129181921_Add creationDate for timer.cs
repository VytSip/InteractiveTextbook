using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InteractiveTextbook.Data.Migrations
{
    public partial class AddcreationDatefortimer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RecordCreatedOn",
                table: "PageTimers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RecordCreatedOn",
                table: "PageTimers");
        }
    }
}
