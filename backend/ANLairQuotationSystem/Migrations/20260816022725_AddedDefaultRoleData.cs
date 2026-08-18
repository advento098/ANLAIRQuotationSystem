using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ANLairQuotationSystem.Migrations
{
    /// <inheritdoc />
    public partial class AddedDefaultRoleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "roles",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "roles",
                columns: new[] { "id", "date_created", "description", "is_active", "name" },
                values: new object[,]
                {
                    { 1u, new DateTime(2026, 8, 16, 10, 25, 0, 0, DateTimeKind.Unspecified), null, true, "Employee" },
                    { 2u, new DateTime(2026, 8, 16, 10, 25, 0, 0, DateTimeKind.Unspecified), null, true, "Sales Support" },
                    { 3u, new DateTime(2026, 8, 16, 10, 25, 0, 0, DateTimeKind.Unspecified), null, true, "Project Officer" },
                    { 4u, new DateTime(2026, 8, 16, 10, 25, 0, 0, DateTimeKind.Unspecified), null, true, "Senior Manager" },
                    { 5u, new DateTime(2026, 8, 16, 10, 25, 0, 0, DateTimeKind.Unspecified), null, true, "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 1u);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 2u);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 3u);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 4u);

            migrationBuilder.DeleteData(
                table: "roles",
                keyColumn: "id",
                keyValue: 5u);

            migrationBuilder.DropColumn(
                name: "is_active",
                table: "roles");
        }
    }
}
