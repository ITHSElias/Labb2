using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb2.Migrations
{
    /// <inheritdoc />
    public partial class RemovedLibraryCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookLoans_LibraryCards_LibraryCardId",
                table: "BookLoans");

            migrationBuilder.DropTable(
                name: "LibraryCards");

            migrationBuilder.DropIndex(
                name: "IX_BookLoans_LibraryCardId",
                table: "BookLoans");

            migrationBuilder.DropColumn(
                name: "LibraryCardId",
                table: "BookLoans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LibraryCardId",
                table: "BookLoans",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                name: "IX_BookLoans_LibraryCardId",
                table: "BookLoans",
                column: "LibraryCardId");

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
