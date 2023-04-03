using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace crm_software_back.Migrations
{
    /// <inheritdoc />
    public partial class FeedbackForm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FeedbackForms",
                columns: table => new
                {
                    FormId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeedbackForms", x => x.FormId);
                });

            migrationBuilder.CreateTable(
                name: "FormQuestions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    Question = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuestionType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FormQuestions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_FormQuestions_FeedbackForms_FormId",
                        column: x => x.FormId,
                        principalTable: "FeedbackForms",
                        principalColumn: "FormId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Answers",
                columns: table => new
                {
                    AnswerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FormId = table.Column<int>(type: "int", nullable: false),
                    QuestionId = table.Column<int>(type: "int", nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    QuesAnswer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Answers", x => x.AnswerId);
                    table.ForeignKey(
                        name: "FK_Answers_FeedbackForms_FormId",
                        column: x => x.FormId,
                        principalTable: "FeedbackForms",
                        principalColumn: "FormId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Answers_FormQuestions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "FormQuestions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Answers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Answers_FormId",
                table: "Answers",
                column: "FormId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_ProjectId",
                table: "Answers",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Answers_QuestionId",
                table: "Answers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_FormQuestions_FormId",
                table: "FormQuestions",
                column: "FormId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Answers");

            migrationBuilder.DropTable(
                name: "FormQuestions");

            migrationBuilder.DropTable(
                name: "FeedbackForms");
        }
    }
}
