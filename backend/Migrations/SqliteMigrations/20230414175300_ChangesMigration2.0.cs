using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.SqliteMigrations
{
    public partial class ChangesMigration20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MajorCourses",
                table: "MajorCourses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "MajorCourses");

            migrationBuilder.AlterColumn<int>(
                name: "MajorId",
                table: "MajorCourses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "CoreRequirements",
                table: "MajorCourses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MajorRequirements",
                table: "MajorCourses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Prerequisites",
                table: "MajorCourses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MajorCourses",
                table: "MajorCourses",
                column: "MajorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MajorCourses",
                table: "MajorCourses");

            migrationBuilder.DropColumn(
                name: "CoreRequirements",
                table: "MajorCourses");

            migrationBuilder.DropColumn(
                name: "MajorRequirements",
                table: "MajorCourses");

            migrationBuilder.DropColumn(
                name: "Prerequisites",
                table: "MajorCourses");

            migrationBuilder.AlterColumn<int>(
                name: "MajorId",
                table: "MajorCourses",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<string>(
                name: "CourseId",
                table: "MajorCourses",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MajorCourses",
                table: "MajorCourses",
                columns: new[] { "MajorId", "CourseId" });
        }
    }
}
