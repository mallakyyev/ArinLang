using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class BagDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 215, DateTimeKind.Local).AddTicks(6313),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 598, DateTimeKind.Local).AddTicks(6883));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 213, DateTimeKind.Local).AddTicks(6206),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 595, DateTimeKind.Local).AddTicks(9316));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 206, DateTimeKind.Local).AddTicks(5331),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 587, DateTimeKind.Local).AddTicks(4661));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 229, DateTimeKind.Local).AddTicks(2144),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 611, DateTimeKind.Local).AddTicks(7390));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Bags",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 598, DateTimeKind.Local).AddTicks(6883),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 215, DateTimeKind.Local).AddTicks(6313));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 595, DateTimeKind.Local).AddTicks(9316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 213, DateTimeKind.Local).AddTicks(6206));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 587, DateTimeKind.Local).AddTicks(4661),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 206, DateTimeKind.Local).AddTicks(5331));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 32, 14, 611, DateTimeKind.Local).AddTicks(7390),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 229, DateTimeKind.Local).AddTicks(2144));

            migrationBuilder.AlterColumn<string>(
                name: "Date",
                table: "Bags",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }
    }
}
