using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PostsServerCore3.Migrations
{
    public partial class Images : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfilePicId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Bytes = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ProfilePicId",
                table: "AspNetUsers",
                column: "ProfilePicId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Images_ProfilePicId",
                table: "AspNetUsers",
                column: "ProfilePicId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Images_ProfilePicId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ProfilePicId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ProfilePicId",
                table: "AspNetUsers");
        }
    }
}
