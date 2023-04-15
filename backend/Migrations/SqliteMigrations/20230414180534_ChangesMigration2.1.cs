using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.SqliteMigrations
{
    public partial class ChangesMigration21 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoreRequirementsCredits",
                table: "MajorCourses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MajorRequirementsCredits",
                table: "MajorCourses",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PrerequisitesCredits",
                table: "MajorCourses",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoreRequirementsCredits",
                table: "MajorCourses");

            migrationBuilder.DropColumn(
                name: "MajorRequirementsCredits",
                table: "MajorCourses");

            migrationBuilder.DropColumn(
                name: "PrerequisitesCredits",
                table: "MajorCourses");
        }
    }
}
