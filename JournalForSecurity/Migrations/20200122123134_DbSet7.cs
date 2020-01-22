using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalForSecurity.Migrations
{
    public partial class DbSet7 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Desc",
                table: "Journals");

            migrationBuilder.AddColumn<int>(
                name: "DescId",
                table: "Journals",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ExplanatoryNotes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(nullable: true),
                    Explanation = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExplanatoryNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExplanatoryNotes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Journals_DescId",
                table: "Journals",
                column: "DescId");

            migrationBuilder.CreateIndex(
                name: "IX_ExplanatoryNotes_UserId",
                table: "ExplanatoryNotes",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_ExplanatoryNotes_DescId",
                table: "Journals",
                column: "DescId",
                principalTable: "ExplanatoryNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_ExplanatoryNotes_DescId",
                table: "Journals");

            migrationBuilder.DropTable(
                name: "ExplanatoryNotes");

            migrationBuilder.DropIndex(
                name: "IX_Journals_DescId",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "DescId",
                table: "Journals");

            migrationBuilder.AddColumn<string>(
                name: "Desc",
                table: "Journals",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
