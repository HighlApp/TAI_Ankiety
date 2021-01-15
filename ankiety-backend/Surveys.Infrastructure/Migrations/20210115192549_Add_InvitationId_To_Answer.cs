using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Surveys.Infrastructure.Migrations
{
    public partial class Add_InvitationId_To_Answer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InvitationId",
                table: "Answers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InvitationId",
                table: "Answers");
        }
    }
}
