using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConflictRenewal.Data.Migrations
{
    public partial class Journal_ConflictStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ConflictStatus",
                table: "Journal",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "MostrecentjournalDate",
                table: "Conflict",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConflictStatus",
                table: "Journal");

            migrationBuilder.AlterColumn<DateTime>(
                name: "MostrecentjournalDate",
                table: "Conflict",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
