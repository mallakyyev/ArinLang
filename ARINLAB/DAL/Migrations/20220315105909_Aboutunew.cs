using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Aboutunew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tittle",
                table: "Aboutus",
                newName: "TittleTM");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Aboutus",
                newName: "TittleRU");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEN",
                table: "Aboutus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionRU",
                table: "Aboutus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionTM",
                table: "Aboutus",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TittleEN",
                table: "Aboutus",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DescriptionEN",
                table: "Aboutus");

            migrationBuilder.DropColumn(
                name: "DescriptionRU",
                table: "Aboutus");

            migrationBuilder.DropColumn(
                name: "DescriptionTM",
                table: "Aboutus");

            migrationBuilder.DropColumn(
                name: "TittleEN",
                table: "Aboutus");

            migrationBuilder.RenameColumn(
                name: "TittleTM",
                table: "Aboutus",
                newName: "Tittle");

            migrationBuilder.RenameColumn(
                name: "TittleRU",
                table: "Aboutus",
                newName: "Description");
        }
    }
}
