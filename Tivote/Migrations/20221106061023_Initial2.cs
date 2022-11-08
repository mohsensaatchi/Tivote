using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tivote.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "TitleId",
                table: "Contacts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Title",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Title", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_TitleId",
                table: "Contacts",
                column: "TitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Title_TitleId",
                table: "Contacts",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Title_TitleId",
                table: "Contacts");

            migrationBuilder.DropTable(
                name: "Title");

            migrationBuilder.DropIndex(
                name: "IX_Contacts_TitleId",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "TitleId",
                table: "Contacts");
        }
    }
}
