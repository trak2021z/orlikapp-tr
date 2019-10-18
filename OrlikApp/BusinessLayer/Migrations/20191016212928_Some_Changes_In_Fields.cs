using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class Some_Changes_In_Fields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "OpenHour",
                table: "WorkingTimes",
                type: "time(0)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "FieldId",
                table: "WorkingTimes",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CloseHour",
                table: "WorkingTimes",
                type: "time(0)",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "time(0)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Fields",
                type: "nvarchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Fields",
                type: "nvarchar(512)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Fields",
                type: "nvarchar(128)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(120)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "OpenHour",
                table: "WorkingTimes",
                type: "time(0)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(0)");

            migrationBuilder.AlterColumn<long>(
                name: "FieldId",
                table: "WorkingTimes",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CloseHour",
                table: "WorkingTimes",
                type: "time(0)",
                nullable: true,
                oldClrType: typeof(TimeSpan),
                oldType: "time(0)");

            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Fields",
                type: "nvarchar(120)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Fields",
                type: "nvarchar(500)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(512)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Fields",
                type: "nvarchar(120)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)");
        }
    }
}
