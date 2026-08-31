using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ANLairQuotationSystem.Migrations
{
    /// <inheritdoc />
    public partial class MappingForQuotationComputationConstant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "quotation_computation_constants",
                columns: table => new
                {
                    quotation_id = table.Column<uint>(type: "int unsigned", nullable: false),
                    computation_constant_id = table.Column<uint>(type: "int unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_quotation_computation_constants", x => new { x.quotation_id, x.computation_constant_id });
                    table.ForeignKey(
                        name: "fk_quotation_computation_constants_computation_constants_comput",
                        column: x => x.computation_constant_id,
                        principalTable: "computation_constants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_quotation_computation_constants_quotations_quotation_id",
                        column: x => x.quotation_id,
                        principalTable: "quotations",
                        principalColumn: "project_id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_quotation_computation_constants_computation_constant_id",
                table: "quotation_computation_constants",
                column: "computation_constant_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "quotation_computation_constants");
        }
    }
}
