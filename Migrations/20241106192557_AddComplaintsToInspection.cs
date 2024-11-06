using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webNET_2024_aspnet_1.Migrations
{
    /// <inheritdoc />
    public partial class AddComplaintsToInspection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Complaints",
                table: "Inspections",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Complaints",
                table: "Inspections");
        }
    }
}
