using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalForSecurity.Migrations
{
    public partial class DbSet10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "Journals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Comment",
                table: "Journals");
        }
    }
}
