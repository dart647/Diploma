using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalForSecurity.Migrations
{
    public partial class DbSet4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Journals_JournalId",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_JournalId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "JournalId",
                table: "Departments");

            migrationBuilder.CreateTable(
                name: "JournalRow",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(nullable: true),
                    JournalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JournalRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JournalRow_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_JournalRow_Journals_JournalId",
                        column: x => x.JournalId,
                        principalTable: "Journals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_JournalRow_DepartmentId",
                table: "JournalRow",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_JournalRow_JournalId",
                table: "JournalRow",
                column: "JournalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JournalRow");

            migrationBuilder.AddColumn<int>(
                name: "JournalId",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_JournalId",
                table: "Departments",
                column: "JournalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Journals_JournalId",
                table: "Departments",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
