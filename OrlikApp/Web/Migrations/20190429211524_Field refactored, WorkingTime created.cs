using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Web.Migrations
{
    public partial class FieldrefactoredWorkingTimecreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Users_KeeperId",
                table: "Fields");

            migrationBuilder.AlterColumn<long>(
                name: "KeeperId",
                table: "Fields",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "WorkingTimes",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Day = table.Column<int>(nullable: false),
                    OpenHour = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    CloseHour = table.Column<TimeSpan>(type: "time(0)", nullable: true),
                    FieldId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingTimes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkingTimes_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkingTimes_FieldId",
                table: "WorkingTimes",
                column: "FieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Users_KeeperId",
                table: "Fields",
                column: "KeeperId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Users_KeeperId",
                table: "Fields");

            migrationBuilder.DropTable(
                name: "WorkingTimes");

            migrationBuilder.AlterColumn<long>(
                name: "KeeperId",
                table: "Fields",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Users_KeeperId",
                table: "Fields",
                column: "KeeperId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
