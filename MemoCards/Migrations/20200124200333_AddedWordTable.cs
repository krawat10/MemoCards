using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemoCards.Migrations
{
    public partial class AddedWordTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Obsolete = table.Column<bool>(nullable: false),
                    Language = table.Column<short>(type: "smallint", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    Key = table.Column<string>(type: "nvarchar(30)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
