using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalForSecurity.Migrations
{
    public partial class DontRequests : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardTasks_ExplanatoryNotes_ExplanatoryId",
                table: "CardTasks");

            migrationBuilder.DropTable(
                name: "CardRequests");

            migrationBuilder.DropIndex(
                name: "IX_CardTasks_ExplanatoryId",
                table: "CardTasks");

            migrationBuilder.DropColumn(
                name: "ExplanatoryId",
                table: "CardTasks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExplanatoryId",
                table: "CardTasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CardRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Answer = table.Column<bool>(type: "bit", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardRequests_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardTasks_ExplanatoryId",
                table: "CardTasks",
                column: "ExplanatoryId");

            migrationBuilder.CreateIndex(
                name: "IX_CardRequests_DepartmentId",
                table: "CardRequests",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CardRequests_UserId",
                table: "CardRequests",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardTasks_ExplanatoryNotes_ExplanatoryId",
                table: "CardTasks",
                column: "ExplanatoryId",
                principalTable: "ExplanatoryNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
