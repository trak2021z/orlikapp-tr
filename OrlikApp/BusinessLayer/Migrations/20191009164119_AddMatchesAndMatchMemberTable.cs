using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BusinessLayer.Migrations
{
    public partial class AddMatchesAndMatchMemberTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Users_KeeperId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingTimes_Fields_FieldId",
                table: "WorkingTimes");

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Descrition = table.Column<string>(type: "nvarchar(256)", nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndOfJoiningDate = table.Column<DateTime>(nullable: false),
                    Result = table.Column<string>(nullable: true),
                    Minutes = table.Column<int>(nullable: true),
                    WantedPlayersAmmount = table.Column<int>(nullable: false),
                    PlayersAmmount = table.Column<int>(nullable: true),
                    IsAccepted = table.Column<bool>(nullable: false),
                    FieldId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: "Fields",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MatchMembers",
                columns: table => new
                {
                    MatchId = table.Column<long>(nullable: false),
                    PlayerId = table.Column<long>(nullable: false),
                    JoiningDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchMembers", x => new { x.MatchId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_MatchMembers_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MatchMembers_Users_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FieldId",
                table: "Matches",
                column: "FieldId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchMembers_PlayerId",
                table: "MatchMembers",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Users_KeeperId",
                table: "Fields",
                column: "KeeperId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingTimes_Fields_FieldId",
                table: "WorkingTimes",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fields_Users_KeeperId",
                table: "Fields");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkingTimes_Fields_FieldId",
                table: "WorkingTimes");

            migrationBuilder.DropTable(
                name: "MatchMembers");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.AddForeignKey(
                name: "FK_Fields_Users_KeeperId",
                table: "Fields",
                column: "KeeperId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_RoleId",
                table: "Users",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkingTimes_Fields_FieldId",
                table: "WorkingTimes",
                column: "FieldId",
                principalTable: "Fields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
