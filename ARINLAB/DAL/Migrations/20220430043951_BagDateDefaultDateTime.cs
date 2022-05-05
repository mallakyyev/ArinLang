using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class BagDateDefaultDateTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 766, DateTimeKind.Local).AddTicks(8521),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 215, DateTimeKind.Local).AddTicks(6313));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 764, DateTimeKind.Local).AddTicks(4642),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 213, DateTimeKind.Local).AddTicks(6206));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 760, DateTimeKind.Local).AddTicks(4405),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 206, DateTimeKind.Local).AddTicks(5331));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 780, DateTimeKind.Local).AddTicks(1659),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 229, DateTimeKind.Local).AddTicks(2144));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Bags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 743, DateTimeKind.Local).AddTicks(6846),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 215, DateTimeKind.Local).AddTicks(6313),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 766, DateTimeKind.Local).AddTicks(8521));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 213, DateTimeKind.Local).AddTicks(6206),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 764, DateTimeKind.Local).AddTicks(4642));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 206, DateTimeKind.Local).AddTicks(5331),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 760, DateTimeKind.Local).AddTicks(4405));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 4, 30, 9, 34, 12, 229, DateTimeKind.Local).AddTicks(2144),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 780, DateTimeKind.Local).AddTicks(1659));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Bags",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 4, 30, 9, 39, 50, 743, DateTimeKind.Local).AddTicks(6846));
        }
    }
}
