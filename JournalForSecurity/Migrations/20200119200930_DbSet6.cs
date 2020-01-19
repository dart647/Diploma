using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalForSecurity.Migrations
{
    public partial class DbSet6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardEvents_AspNetUsers_UserId1",
                table: "CardEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_CardRequests_AspNetUsers_UserId1",
                table: "CardRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CardTasks_AspNetUsers_UserId1",
                table: "CardTasks");

            migrationBuilder.DropIndex(
                name: "IX_CardTasks_UserId1",
                table: "CardTasks");

            migrationBuilder.DropIndex(
                name: "IX_CardRequests_UserId1",
                table: "CardRequests");

            migrationBuilder.DropIndex(
                name: "IX_CardEvents_UserId1",
                table: "CardEvents");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CardTasks");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CardRequests");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "CardEvents");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CardTasks",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CardRequests",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "CardEvents",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardTasks_UserId",
                table: "CardTasks",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardRequests_UserId",
                table: "CardRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardEvents_UserId",
                table: "CardEvents",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardEvents_AspNetUsers_UserId",
                table: "CardEvents",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardRequests_AspNetUsers_UserId",
                table: "CardRequests",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardTasks_AspNetUsers_UserId",
                table: "CardTasks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardEvents_AspNetUsers_UserId",
                table: "CardEvents");

            migrationBuilder.DropForeignKey(
                name: "FK_CardRequests_AspNetUsers_UserId",
                table: "CardRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CardTasks_AspNetUsers_UserId",
                table: "CardTasks");

            migrationBuilder.DropIndex(
                name: "IX_CardTasks_UserId",
                table: "CardTasks");

            migrationBuilder.DropIndex(
                name: "IX_CardRequests_UserId",
                table: "CardRequests");

            migrationBuilder.DropIndex(
                name: "IX_CardEvents_UserId",
                table: "CardEvents");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CardTasks",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "CardTasks",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CardRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "CardRequests",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "CardEvents",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "CardEvents",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardTasks_UserId1",
                table: "CardTasks",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CardRequests_UserId1",
                table: "CardRequests",
                column: "UserId1");

            migrationBuilder.CreateIndex(
                name: "IX_CardEvents_UserId1",
                table: "CardEvents",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_CardEvents_AspNetUsers_UserId1",
                table: "CardEvents",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardRequests_AspNetUsers_UserId1",
                table: "CardRequests",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CardTasks_AspNetUsers_UserId1",
                table: "CardTasks",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
