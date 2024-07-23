using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEndDevChallenge.Migrations
{
    /// <inheritdoc />
    public partial class ModifyMathProblem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Result",
                table: "MathProblems",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ResultType",
                table: "MathProblems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "MathProblems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultType",
                table: "MathProblems");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "MathProblems");

            migrationBuilder.AlterColumn<int>(
                name: "Result",
                table: "MathProblems",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
