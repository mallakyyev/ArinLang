using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Dictionary_FooterLogo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 991, DateTimeKind.Local).AddTicks(2082),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 789, DateTimeKind.Local).AddTicks(6318));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 989, DateTimeKind.Local).AddTicks(2568),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 787, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 986, DateTimeKind.Local).AddTicks(2627),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 784, DateTimeKind.Local).AddTicks(7635));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 2, 0, 18, 16, 1, DateTimeKind.Local).AddTicks(5008),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 799, DateTimeKind.Local).AddTicks(5761));

            migrationBuilder.AddColumn<string>(
                name: "FooterLogo",
                table: "Dictionaries",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Bags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 974, DateTimeKind.Local).AddTicks(5316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 773, DateTimeKind.Local).AddTicks(5376));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FooterLogo",
                table: "Dictionaries");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 789, DateTimeKind.Local).AddTicks(6318),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 991, DateTimeKind.Local).AddTicks(2082));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 787, DateTimeKind.Local).AddTicks(7083),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 989, DateTimeKind.Local).AddTicks(2568));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 784, DateTimeKind.Local).AddTicks(7635),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 986, DateTimeKind.Local).AddTicks(2627));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 799, DateTimeKind.Local).AddTicks(5761),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 2, 0, 18, 16, 1, DateTimeKind.Local).AddTicks(5008));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Bags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 773, DateTimeKind.Local).AddTicks(5376),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 974, DateTimeKind.Local).AddTicks(5316));
        }
    }
}
