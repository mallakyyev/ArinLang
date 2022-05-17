using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class BagIsRead : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 9, 3, 18, 115, DateTimeKind.Local).AddTicks(271),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 991, DateTimeKind.Local).AddTicks(2082));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 9, 3, 18, 112, DateTimeKind.Local).AddTicks(4615),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 989, DateTimeKind.Local).AddTicks(2568));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 9, 3, 18, 109, DateTimeKind.Local).AddTicks(3191),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 986, DateTimeKind.Local).AddTicks(2627));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 9, 3, 18, 126, DateTimeKind.Local).AddTicks(1445),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 2, 0, 18, 16, 1, DateTimeKind.Local).AddTicks(5008));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Bags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 17, 9, 3, 18, 96, DateTimeKind.Local).AddTicks(8232),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 974, DateTimeKind.Local).AddTicks(5316));

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Bags",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Bags");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordSentences",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 991, DateTimeKind.Local).AddTicks(2082),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 17, 9, 3, 18, 115, DateTimeKind.Local).AddTicks(271));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Words",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 989, DateTimeKind.Local).AddTicks(2568),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 17, 9, 3, 18, 112, DateTimeKind.Local).AddTicks(4615));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "WordClauses",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 986, DateTimeKind.Local).AddTicks(2627),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 17, 9, 3, 18, 109, DateTimeKind.Local).AddTicks(3191));

            migrationBuilder.AlterColumn<DateTime>(
                name: "AddedDate",
                table: "Names",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 2, 0, 18, 16, 1, DateTimeKind.Local).AddTicks(5008),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 17, 9, 3, 18, 126, DateTimeKind.Local).AddTicks(1445));

            migrationBuilder.AlterColumn<DateTime>(
                name: "Date",
                table: "Bags",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2022, 5, 2, 0, 18, 15, 974, DateTimeKind.Local).AddTicks(5316),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValue: new DateTime(2022, 5, 17, 9, 3, 18, 96, DateTimeKind.Local).AddTicks(8232));
        }
    }
}
