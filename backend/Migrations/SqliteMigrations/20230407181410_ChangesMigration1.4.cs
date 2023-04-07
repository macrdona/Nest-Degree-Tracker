using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.SqliteMigrations
{
    public partial class ChangesMigration14 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Course",
                table: "CompletedCourses",
                newName: "CourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseId",
                table: "CompletedCourses",
                newName: "Course");
        }
    }
}
