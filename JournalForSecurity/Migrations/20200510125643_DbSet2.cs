using Microsoft.EntityFrameworkCore.Migrations;

namespace JournalForSecurity.Migrations
{
    public partial class DbSet2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CardEvents_ExplanatoryNotes_ExplanatoryId",
                table: "CardEvents");

            migrationBuilder.DropIndex(
                name: "IX_CardEvents_ExplanatoryId",
                table: "CardEvents");

            migrationBuilder.DropColumn(
                name: "ExplanatoryId",
                table: "CardEvents");

            migrationBuilder.AddColumn<string>(
                name: "AlertResult",
                table: "CardEvents",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                maxLength: 11,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlertResult",
                table: "CardEvents");

            migrationBuilder.AddColumn<int>(
                name: "ExplanatoryId",
                table: "CardEvents",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 11,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CardEvents_ExplanatoryId",
                table: "CardEvents",
                column: "ExplanatoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CardEvents_ExplanatoryNotes_ExplanatoryId",
                table: "CardEvents",
                column: "ExplanatoryId",
                principalTable: "ExplanatoryNotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
