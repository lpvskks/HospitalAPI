using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webNET_2024_aspnet_1.Migrations
{
    /// <inheritdoc />
    public partial class AddedHasChainHasNested : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasChain",
                table: "Inspections",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasNested",
                table: "Inspections",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasChain",
                table: "Inspections");

            migrationBuilder.DropColumn(
                name: "HasNested",
                table: "Inspections");
        }
    }
}
