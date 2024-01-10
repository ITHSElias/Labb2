﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Labb2.Migrations
{
    /// <inheritdoc />
    public partial class AddedIsbnUnqConstraint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Books_Isbn",
                table: "Books",
                column: "Isbn",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Books_Isbn",
                table: "Books");
        }
    }
}
