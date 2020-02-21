using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalForSecurity.Migrations
{
    public partial class DbSet13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Journals_ExplanatoryNotes_DescId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_DescId",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "DescId",
                table: "Journals");

            migrationBuilder.AddColumn<int>(
                name: "ExplanationId",
                table: "Journals",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ExplanationId",
                table: "CardTasks",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journals_ExplanationId",
                table: "Journals",
                column: "ExplanationId");

            migrationBuilder.CreateIndex(
                name: "IX_CardTasks_ExplanationId",
                table: "CardTasks",
                column: "ExplanationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardTasks_ExplanatoryNotes_ExplanationId",
                table: "CardTasks",
                column: "ExplanationId",
                principalTable: "ExplanatoryNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_ExplanatoryNotes_ExplanationId",
                table: "Journals",
                column: "ExplanationId",
                principalTable: "ExplanatoryNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardTasks_ExplanatoryNotes_ExplanationId",
                table: "CardTasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Journals_ExplanatoryNotes_ExplanationId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_Journals_ExplanationId",
                table: "Journals");

            migrationBuilder.DropIndex(
                name: "IX_CardTasks_ExplanationId",
                table: "CardTasks");

            migrationBuilder.DropColumn(
                name: "ExplanationId",
                table: "Journals");

            migrationBuilder.DropColumn(
                name: "ExplanationId",
                table: "CardTasks");

            migrationBuilder.AddColumn<int>(
                name: "DescId",
                table: "Journals",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Journals_DescId",
                table: "Journals",
                column: "DescId");

            migrationBuilder.AddForeignKey(
                name: "FK_Journals_ExplanatoryNotes_DescId",
                table: "Journals",
                column: "DescId",
                principalTable: "ExplanatoryNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
