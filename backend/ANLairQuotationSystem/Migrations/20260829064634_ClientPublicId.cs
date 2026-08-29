using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ANLairQuotationSystem.Migrations
{
    /// <inheritdoc />
    public partial class ClientPublicId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "public_id",
                table: "clients",
                type: "char(15)",
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "ix_clients_public_id",
                table: "clients",
                column: "public_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_clients_public_id",
                table: "clients");

            migrationBuilder.DropColumn(
                name: "public_id",
                table: "clients");
        }
    }
}
