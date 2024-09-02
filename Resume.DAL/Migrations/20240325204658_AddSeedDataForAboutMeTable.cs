using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resume.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataForAboutMeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AboutMe",
                columns: new[] { "Id", "Bio", "BirthDate", "CreateDate", "Email", "FirstName", "LastName", "Location", "Mobile", "Position" },
                values: new object[] { 1, "", new DateOnly(2024, 3, 26), new DateTime(2024, 3, 26, 0, 16, 55, 380, DateTimeKind.Local).AddTicks(53), "", "", "", "", "", "" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 3, 26, 0, 16, 55, 379, DateTimeKind.Local).AddTicks(9481));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AboutMe",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreateDate",
                value: new DateTime(2024, 3, 26, 0, 9, 48, 541, DateTimeKind.Local).AddTicks(6440));
        }
    }
}
