using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ANLairQuotationSystem.Migrations
{
    /// <inheritdoc />
    public partial class AdddedUserSessionStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_active",
                table: "user_sessions",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_active",
                table: "user_sessions");
        }
    }
}
