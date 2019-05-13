using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class RemoveAddressentity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Addresses_AddressId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Fields_AddressId",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Fields");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Users",
                type: "nvarchar(120)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Users",
                type: "nvarchar(120)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StreetNumber",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Fields",
                type: "nvarchar(120)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Street",
                table: "Fields",
                type: "nvarchar(120)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StreetNumber",
                table: "Fields",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "City",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "Street",
                table: "Fields");

            migrationBuilder.DropColumn(
                name: "StreetNumber",
                table: "Fields");

            migrationBuilder.AddColumn<long>(
                name: "AddressId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AddressId",
                table: "Fields",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    City = table.Column<string>(type: "nvarchar(120)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(120)", nullable: true),
                    StreetNumber = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressId",
                table: "Users",
                column: "AddressId");

            migrationBuilder.CreateIndex(
                name: "IX_Fields_AddressId",
                table: "Fields",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Addresses_AddressId",
                table: "Fields",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressId",
                table: "Users",
                column: "AddressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
