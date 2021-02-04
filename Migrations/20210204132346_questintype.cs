using Microsoft.EntityFrameworkCore.Migrations;

namespace quizmoon.Migrations
{
    public partial class questintype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AnswersType",
                table: "QuizQuestions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "QuizQuestions",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswersType",
                table: "QuizQuestions");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "QuizQuestions");
        }
    }
}
