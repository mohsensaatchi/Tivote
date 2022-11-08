using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tivote.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Title_TitleId",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Title",
                table: "Title");

            migrationBuilder.RenameTable(
                name: "Title",
                newName: "Titles");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Titles",
                table: "Titles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Titles_TitleId",
                table: "Contacts",
                column: "TitleId",
                principalTable: "Titles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contacts_Titles_TitleId",
                table: "Contacts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Titles",
                table: "Titles");

            migrationBuilder.RenameTable(
                name: "Titles",
                newName: "Title");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Title",
                table: "Title",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contacts_Title_TitleId",
                table: "Contacts",
                column: "TitleId",
                principalTable: "Title",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
