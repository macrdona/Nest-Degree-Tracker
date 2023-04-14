using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.SqliteMigrations
{
    public partial class ChangesMigration22 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MajorCourses");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MajorCourses",
                columns: table => new
                {
                    MajorId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CoreRequirements = table.Column<string>(type: "TEXT", nullable: true),
                    CoreRequirementsCredits = table.Column<string>(type: "TEXT", nullable: true),
                    MajorRequirements = table.Column<string>(type: "TEXT", nullable: true),
                    MajorRequirementsCredits = table.Column<string>(type: "TEXT", nullable: true),
                    Prerequisites = table.Column<string>(type: "TEXT", nullable: true),
                    PrerequisitesCredits = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MajorCourses", x => x.MajorId);
                });
        }
    }
}
