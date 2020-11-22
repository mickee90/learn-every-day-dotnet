using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LearnEveryDay.Migrations
{
    public partial class SomeChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a9b1bc21-c51c-4ff8-b37e-dc9452edf74d"),
                column: "ConcurrencyStamp",
                value: "f91ef6f2-fa54-4a2d-9e68-4014d0027e3b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ad6fc1b5-08cb-43e1-a26f-6cb6753b70bf"),
                column: "ConcurrencyStamp",
                value: "4b486779-8881-4a10-89a5-af23306d83c5");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a9b1bc21-c51c-4ff8-b37e-dc9452edf74d"),
                column: "ConcurrencyStamp",
                value: "ea6b3d49-ba75-45a9-9ccc-c3e74bae83fd");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("ad6fc1b5-08cb-43e1-a26f-6cb6753b70bf"),
                column: "ConcurrencyStamp",
                value: "b9482f48-2a93-4436-89da-e416aa27841e");
        }
    }
}
