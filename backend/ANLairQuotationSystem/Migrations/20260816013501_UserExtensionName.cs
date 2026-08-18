using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ANLairQuotationSystem.Migrations
{
    /// <inheritdoc />
    public partial class UserExtensionName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "extension_name",
                table: "users",
                type: "varchar(20)",
                maxLength: 20,
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "extension_name",
                table: "users");
        }
    }
}
