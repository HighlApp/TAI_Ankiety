using Microsoft.EntityFrameworkCore.Migrations;

namespace Surveys.Infrastructure.Migrations
{
    public partial class RenameTextToOptionText : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Options",
                newName: "OptionText");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OptionText",
                table: "Options",
                newName: "Text");
        }
    }
}
