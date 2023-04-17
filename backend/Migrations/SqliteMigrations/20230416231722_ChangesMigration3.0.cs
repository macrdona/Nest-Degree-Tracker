using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations.SqliteMigrations
{
    public partial class ChangesMigration30 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Requirements",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    CompletedCredits = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalCredits = table.Column<int>(type: "INTEGER", nullable: false),
                    Satisfied = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requirements", x => new { x.UserId, x.Name });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Requirements");
        }
    }
}
