using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CleanArchitecture.Infrastructure.Persistence.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Product",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Barcode = table.Column<string>(maxLength: 1000, nullable: false),
                    Description = table.Column<string>(maxLength: 2000, nullable: false),
                    Rate = table.Column<decimal>(nullable: false, defaultValue: 0m)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Colour = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TodoItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    LastModifiedBy = table.Column<string>(maxLength: 255, nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                    ListId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Note = table.Column<string>(nullable: true),
                    Done = table.Column<bool>(nullable: false),
                    Reminder = table.Column<DateTime>(nullable: true),
                    Priority = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoItems_TodoLists_ListId",
                        column: x => x.ListId,
                        principalTable: "TodoLists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoItems_ListId",
                table: "TodoItems",
                column: "ListId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Product");

            migrationBuilder.DropTable(
                name: "TodoItems");

            migrationBuilder.DropTable(
                name: "TodoLists");
        }
    }
}
