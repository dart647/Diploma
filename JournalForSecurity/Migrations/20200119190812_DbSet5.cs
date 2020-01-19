using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalForSecurity.Migrations
{
    public partial class DbSet5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalRow_Departments_DepartmentId",
                table: "JournalRow");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalRow_Journals_JournalId",
                table: "JournalRow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JournalRow",
                table: "JournalRow");

            migrationBuilder.DropIndex(
                name: "IX_JournalRow_JournalId",
                table: "JournalRow");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "JournalRow");

            migrationBuilder.AlterColumn<int>(
                name: "JournalId",
                table: "JournalRow",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "JournalRow",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_JournalRow",
                table: "JournalRow",
                columns: new[] { "JournalId", "DepartmentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_JournalRow_Departments_DepartmentId",
                table: "JournalRow",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalRow_Journals_JournalId",
                table: "JournalRow",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JournalRow_Departments_DepartmentId",
                table: "JournalRow");

            migrationBuilder.DropForeignKey(
                name: "FK_JournalRow_Journals_JournalId",
                table: "JournalRow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JournalRow",
                table: "JournalRow");

            migrationBuilder.AlterColumn<int>(
                name: "DepartmentId",
                table: "JournalRow",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "JournalId",
                table: "JournalRow",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "JournalRow",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JournalRow",
                table: "JournalRow",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_JournalRow_JournalId",
                table: "JournalRow",
                column: "JournalId");

            migrationBuilder.AddForeignKey(
                name: "FK_JournalRow_Departments_DepartmentId",
                table: "JournalRow",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_JournalRow_Journals_JournalId",
                table: "JournalRow",
                column: "JournalId",
                principalTable: "Journals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
