using Microsoft.EntityFrameworkCore.Migrations;

namespace Surveys.Infrastructure.Migrations
{
    public partial class ChangedNameOfSurveyTypeProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SurveyType",
                table: "Surveys",
                newName: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Surveys",
                newName: "SurveyType");
        }
    }
}
