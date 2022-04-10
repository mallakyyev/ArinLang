using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class NamesUserIdForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Names",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Names_UserId",
                table: "Names",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Names_AspNetUsers_UserId",
                table: "Names",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Names_AspNetUsers_UserId",
                table: "Names");

            migrationBuilder.DropIndex(
                name: "IX_Names_UserId",
                table: "Names");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Names",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }
    }
}
