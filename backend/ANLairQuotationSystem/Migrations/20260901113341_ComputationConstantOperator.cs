using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ANLairQuotationSystem.Migrations
{
    /// <inheritdoc />
    public partial class ComputationConstantOperator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "operator",
                table: "computation_constants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "operator",
                table: "computation_constants");
        }
    }
}
