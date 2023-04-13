using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.SqliteMigrations
{
    public partial class ChangesMigration13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CourseId",
                table: "MajorCourses",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Course",
                table: "CompletedCourses",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MajorCourses",
                table: "MajorCourses",
                columns: new[] { "MajorId", "CourseId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompletedCourses",
                table: "CompletedCourses",
                columns: new[] { "UserId", "Course" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MajorCourses",
                table: "MajorCourses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompletedCourses",
                table: "CompletedCourses");

            migrationBuilder.AlterColumn<string>(
                name: "CourseId",
                table: "MajorCourses",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Course",
                table: "CompletedCourses",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
