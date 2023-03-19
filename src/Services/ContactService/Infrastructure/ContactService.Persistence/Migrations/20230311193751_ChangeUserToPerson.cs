using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserToPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformations_Users_UserId",
                table: "ContactInformations");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "ContactInformations",
                newName: "PersonId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactInformations_UserId",
                table: "ContactInformations",
                newName: "IX_ContactInformations_PersonId");

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Company = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformations_Persons_PersonId",
                table: "ContactInformations",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInformations_Persons_PersonId",
                table: "ContactInformations");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.RenameColumn(
                name: "PersonId",
                table: "ContactInformations",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ContactInformations_PersonId",
                table: "ContactInformations",
                newName: "IX_ContactInformations_UserId");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Company = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Surname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInformations_Users_UserId",
                table: "ContactInformations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
