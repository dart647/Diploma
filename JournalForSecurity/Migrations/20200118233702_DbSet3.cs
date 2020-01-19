using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalForSecurity.Migrations
{
    public partial class DbSet3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "CardTasks",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "CardRequests",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "CardEvents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DepartmentId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardTasks_DepartmentId",
                table: "CardTasks",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CardRequests_DepartmentId",
                table: "CardRequests",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CardEvents_DepartmentId",
                table: "CardEvents",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardEvents_Departments_DepartmentId",
                table: "CardEvents",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardRequests_Departments_DepartmentId",
                table: "CardRequests",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardTasks_Departments_DepartmentId",
                table: "CardTasks",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Departments_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CardEvents_Departments_DepartmentId",
                table: "CardEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_CardRequests_Departments_DepartmentId",
                table: "CardRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CardTasks_Departments_DepartmentId",
                table: "CardTasks");

            migrationBuilder.DropIndex(
                name: "IX_CardTasks_DepartmentId",
                table: "CardTasks");

            migrationBuilder.DropIndex(
                name: "IX_CardRequests_DepartmentId",
                table: "CardRequests");

            migrationBuilder.DropIndex(
                name: "IX_CardEvents_DepartmentId",
                table: "CardEvents");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DepartmentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "CardTasks");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "CardRequests");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "CardEvents");

            migrationBuilder.DropColumn(
                name: "DepartmentId",
                table: "AspNetUsers");
        }
    }
}
