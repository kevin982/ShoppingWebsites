using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CategoryWebsite_MS.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebsiteCategories",
                columns: table => new
                {
                    WebsiteCategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WebsiteCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebsiteCategories", x => x.WebsiteCategoryId);
                });

            migrationBuilder.InsertData(
                table: "WebsiteCategories",
                columns: new[] { "WebsiteCategoryId", "WebsiteCategoryName" },
                values: new object[,]
                {
                    { new Guid("453ac685-cd85-433a-abd6-2330edaea4f3"), "Department Store" },
                    { new Guid("fd502319-08eb-4e89-bc79-ce9d242563e4"), "Speciality Store" },
                    { new Guid("59ab7e8b-1472-419d-802f-5cda5c7b238d"), "Supermarket" },
                    { new Guid("4ec94f5a-c25b-4db5-8a0b-a5ed1818339c"), "Convenience Store" },
                    { new Guid("2314e0e3-ea11-46a5-9285-d65b29f61420"), "Discount Store" },
                    { new Guid("5c387ba2-fbad-4e59-bfb8-9416811a133b"), "Hypermarket" },
                    { new Guid("459adfd8-2ca5-40a8-bf0c-212bf99fd0de"), "Warehouse Store" },
                    { new Guid("25c50ed0-d381-4154-bbb4-c509487bc505"), "E-Commerce" },
                    { new Guid("8a692d9d-e284-4153-aeb9-cc54bcc5ba19"), "Drug Store" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebsiteCategories");
        }
    }
}
