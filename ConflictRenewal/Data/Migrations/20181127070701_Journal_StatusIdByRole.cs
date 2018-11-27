using Microsoft.EntityFrameworkCore.Migrations;

namespace ConflictRenewal.Data.Migrations
{
    public partial class Journal_StatusIdByRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusIdByRole",
                table: "Journal",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusIdByRole",
                table: "Journal");
        }
    }
}
