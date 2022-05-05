using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class BugDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 598, DateTimeKind.Local).AddTicks(6883),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 29, 11, 2, 47, 211, DateTimeKind.Local).AddTicks(2802));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 595, DateTimeKind.Local).AddTicks(9316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 29, 11, 2, 47, 209, DateTimeKind.Local).AddTicks(1428));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 587, DateTimeKind.Local).AddTicks(4661),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 29, 11, 2, 47, 200, DateTimeKind.Local).AddTicks(6241));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 611, DateTimeKind.Local).AddTicks(7390),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 29, 11, 2, 47, 222, DateTimeKind.Local).AddTicks(2961));

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Bags",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Bags");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 11, 2, 47, 211, DateTimeKind.Local).AddTicks(2802),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 598, DateTimeKind.Local).AddTicks(6883));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 11, 2, 47, 209, DateTimeKind.Local).AddTicks(1428),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 595, DateTimeKind.Local).AddTicks(9316));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 11, 2, 47, 200, DateTimeKind.Local).AddTicks(6241),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 587, DateTimeKind.Local).AddTicks(4661));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 29, 11, 2, 47, 222, DateTimeKind.Local).AddTicks(2961),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 611, DateTimeKind.Local).AddTicks(7390));
        }
    }
}
