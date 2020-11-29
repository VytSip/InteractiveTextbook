using Microsoft.EntityFrameworkCore.Migrations;

namespace InteractiveTextbook.Data.Migrations
{
    public partial class AddedaudioPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AudioPath",
                table: "Pages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AudioPath",
                table: "Pages");
        }
    }
}
