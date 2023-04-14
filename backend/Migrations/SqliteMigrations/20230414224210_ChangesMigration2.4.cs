using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.SqliteMigrations
{
    public partial class ChangesMigration24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requirements",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StateRequirements = table.Column<bool>(type: "INTEGER", nullable: false),
                    UNFRequirements = table.Column<bool>(type: "INTEGER", nullable: false),
                    OralRequirement = table.Column<bool>(type: "INTEGER", nullable: false),
                    Prerequisites = table.Column<bool>(type: "INTEGER", nullable: false),
                    CoreRequirements = table.Column<bool>(type: "INTEGER", nullable: false),
                    MajorRequirements = table.Column<bool>(type: "INTEGER", nullable: false),
                    MajorElectives = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirements", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requirements");
        }
    }
}
