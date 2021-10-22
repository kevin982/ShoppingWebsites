using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityMicroservice.Migrations
{
    public partial class NewRolesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4dcaa670-bbd6-4be8-8218-a151af4eec44");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "90e363c9-6193-4c02-a810-8fe356908125");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "14d699cc-7136-4b13-b5de-0a6d679e70c8", "6bb69995-6620-450d-bf13-c34d04a56ecc", "costumer", "COSTUMER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e3490a78-942b-44d7-be72-1e96bc6e99f7", "325bb073-b263-493b-b4cf-54bfb81629ba", "owner", "OWNER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c8829851-bb85-4a35-b2a8-5b6998e141b3", "d39f350e-b62a-4c6b-9f41-5b5650a259ba", "admin", "ADMIN" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "14d699cc-7136-4b13-b5de-0a6d679e70c8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8829851-bb85-4a35-b2a8-5b6998e141b3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e3490a78-942b-44d7-be72-1e96bc6e99f7");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "4dcaa670-bbd6-4be8-8218-a151af4eec44", "2e7ec7c3-f256-4204-900b-b05cb7831a29", "user", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "90e363c9-6193-4c02-a810-8fe356908125", "c2259e5e-d581-48b1-9638-238d05312001", "admin", "ADMIN" });
        }
    }
}
