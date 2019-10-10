using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class Modify_WorkingTime_Field_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FieldId",
                table: "WorkingTimes",
                nullable: true,
                oldClrType: typeof(long));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "FieldId",
                table: "WorkingTimes",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }
    }
}
