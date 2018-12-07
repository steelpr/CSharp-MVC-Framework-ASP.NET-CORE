using Microsoft.EntityFrameworkCore.Migrations;

namespace ExchangeRate.Data.Migrations
{
    public partial class ArticleAndCurrencyAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "DateOfPuplish",
                table: "Articles",
                newName: "DateOfPublish");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Articles",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "DateOfPublish",
                table: "Articles",
                newName: "DateOfPuplish");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Articles",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddForeignKey(
                name: "FK_Articles_AspNetUsers_UserId",
                table: "Articles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
