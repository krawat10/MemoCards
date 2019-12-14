using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemoCards.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Obsolete = table.Column<bool>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Password_Hash = table.Column<byte[]>(nullable: true),
                    Password_Salt = table.Column<byte[]>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MemoCards",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Obsolete = table.Column<bool>(nullable: false),
                    UserId = table.Column<Guid>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemoCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MemoCards_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemoCards_UserId",
                table: "MemoCards",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemoCards");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
