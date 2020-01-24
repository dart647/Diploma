using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalForSecurity.Migrations
{
    public partial class DbSet9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalRow");

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "Journals",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journals_DepartmentId",
                table: "Journals",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_Departments_DepartmentId",
                table: "Journals",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_Departments_DepartmentId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_DepartmentId",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "Journals");

            migrationBuilder.CreateTable(
                name: "JournalRow",
                columns: table => new
                {
                    JournalId = table.Column<int>(type: "int", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalRow", x => new { x.JournalId, x.DepartmentId });
                    table.ForeignKey(
                        name: "FK_JournalRow_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JournalRow_Journals_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalRow_DepartmentId",
                table: "JournalRow",
                column: "DepartmentId");
        }
    }
}
