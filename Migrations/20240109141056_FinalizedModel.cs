using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb2.Migrations
{
    /// <inheritdoc />
    public partial class FinalizedModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_LibraryCards_LibraryCardId",
                table: "BookLoans");

            migrationBuilder.DropTable(
                name: "LibraryCards");

            migrationBuilder.RenameColumn(
                name: "LibraryCardId",
                table: "BookLoans",
                newName: "CustomerId");

            migrationBuilder.RenameIndex(
                name: "IX_BookLoans_LibraryCardId",
                table: "BookLoans",
                newName: "IX_BookLoans_CustomerId");

            migrationBuilder.AddColumn<bool>(
                name: "HasValidLibraryCard",
                table: "Customers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_Customers_CustomerId",
                table: "BookLoans",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_Customers_CustomerId",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "HasValidLibraryCard",
                table: "Customers");

            migrationBuilder.RenameColumn(
                name: "CustomerId",
                table: "BookLoans",
                newName: "LibraryCardId");

            migrationBuilder.RenameIndex(
                name: "IX_BookLoans_CustomerId",
                table: "BookLoans",
                newName: "IX_BookLoans_LibraryCardId");

            migrationBuilder.CreateTable(
                name: "LibraryCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ExpirationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LibraryCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LibraryCards_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LibraryCards_CustomerId",
                table: "LibraryCards",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookLoans_LibraryCards_LibraryCardId",
                table: "BookLoans",
                column: "LibraryCardId",
                principalTable: "LibraryCards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
