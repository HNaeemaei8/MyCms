using Microsoft.EntityFrameworkCore.Migrations;

namespace MyCms.Domain.Migrations
{
    public partial class RelationPage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageComments_Pages_PageId",
                table: "PageComments");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "PageComments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageComments_UserId",
                table: "PageComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageComments_Pages_PageId",
                table: "PageComments",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageComments_Users_UserId",
                table: "PageComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageComments_Pages_PageId",
                table: "PageComments");

            migrationBuilder.DropForeignKey(
                name: "FK_PageComments_Users_UserId",
                table: "PageComments");

            migrationBuilder.DropIndex(
                name: "IX_PageComments_UserId",
                table: "PageComments");

            migrationBuilder.AlterColumn<int>(
                name: "PageId",
                table: "PageComments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_PageComments_Pages_PageId",
                table: "PageComments",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "PageId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
