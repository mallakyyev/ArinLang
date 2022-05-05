using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class Dictionary_isActiveAddded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 789, DateTimeKind.Local).AddTicks(6318),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 766, DateTimeKind.Local).AddTicks(8521));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 787, DateTimeKind.Local).AddTicks(7083),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 764, DateTimeKind.Local).AddTicks(4642));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 784, DateTimeKind.Local).AddTicks(7635),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 760, DateTimeKind.Local).AddTicks(4405));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 799, DateTimeKind.Local).AddTicks(5761),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 780, DateTimeKind.Local).AddTicks(1659));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Dictionaries",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Bags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 773, DateTimeKind.Local).AddTicks(5376),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 743, DateTimeKind.Local).AddTicks(6846));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Dictionaries");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 766, DateTimeKind.Local).AddTicks(8521),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 789, DateTimeKind.Local).AddTicks(6318));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 764, DateTimeKind.Local).AddTicks(4642),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 787, DateTimeKind.Local).AddTicks(7083));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 760, DateTimeKind.Local).AddTicks(4405),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 784, DateTimeKind.Local).AddTicks(7635));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 780, DateTimeKind.Local).AddTicks(1659),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 799, DateTimeKind.Local).AddTicks(5761));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Bags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 743, DateTimeKind.Local).AddTicks(6846),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 1, 23, 49, 14, 773, DateTimeKind.Local).AddTicks(5376));
        }
    }
}
