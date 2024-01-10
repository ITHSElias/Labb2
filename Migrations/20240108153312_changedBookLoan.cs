using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb2.Migrations
{
    /// <inheritdoc />
    public partial class changedBookLoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_Books_BookId",
                table: "BookLoans");

            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "BookLoans",
                newName: "BookCopyId");

            migrationBuilder.RenameIndex(
                name: "IX_BookLoans_BookId",
                table: "BookLoans",
                newName: "IX_BookLoans_BookCopyId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_BookCopiesInLibrary_BookCopyId",
                table: "BookLoans",
                column: "BookCopyId",
                principalTable: "BookCopiesInLibrary",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_BookCopiesInLibrary_BookCopyId",
                table: "BookLoans");

            migrationBuilder.RenameColumn(
                name: "BookCopyId",
                table: "BookLoans",
                newName: "BookId");

            migrationBuilder.RenameIndex(
                name: "IX_BookLoans_BookCopyId",
                table: "BookLoans",
                newName: "IX_BookLoans_BookId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_Books_BookId",
                table: "BookLoans",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
