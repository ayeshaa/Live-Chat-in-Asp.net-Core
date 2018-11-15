using Microsoft.EntityFrameworkCore.Migrations;

namespace ConflictRenewal.Data.Migrations
{
    public partial class Journal_createdBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "createdBy",
                table: "Journal",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdBy",
                table: "Journal");
        }
    }
}
