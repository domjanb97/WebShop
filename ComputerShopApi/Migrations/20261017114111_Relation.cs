using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComputerShopApi.Migrations
{
    /// <inheritdoc />
    public partial class Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OsId",
                table: "Computers",
                type: "char(36)",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                collation: "ascii_general_ci");

            migrationBuilder.CreateIndex(
                name: "IX_Computers_OsId",
                table: "Computers",
                column: "OsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Computers_Osystems_OsId",
                table: "Computers",
                column: "OsId",
                principalTable: "Osystems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Computers_Osystems_OsId",
                table: "Computers");

            migrationBuilder.DropIndex(
                name: "IX_Computers_OsId",
                table: "Computers");

            migrationBuilder.DropColumn(
                name: "OsId",
                table: "Computers");
        }
    }
}
