using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ViewedAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Viewed",
                table: "Words",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Viewed",
                table: "WordClauses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Viewed",
                table: "Names",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Viewed",
                table: "NameImages",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "Words");

            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "WordClauses");

            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "Names");

            migrationBuilder.DropColumn(
                name: "Viewed",
                table: "NameImages");
        }
    }
}
