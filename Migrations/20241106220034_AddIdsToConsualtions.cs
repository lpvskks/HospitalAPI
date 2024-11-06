using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webNET_2024_aspnet_1.Migrations
{
    /// <inheritdoc />
    public partial class AddIdsToConsualtions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_InspectionComments_RootCommentId1",
                table: "Consultations");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_RootCommentId1",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "RootCommentId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "RootCommentId1",
                table: "Consultations");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionComments_ConsultationId",
                table: "InspectionComments",
                column: "ConsultationId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionComments_Consultations_ConsultationId",
                table: "InspectionComments",
                column: "ConsultationId",
                principalTable: "Consultations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InspectionComments_Consultations_ConsultationId",
                table: "InspectionComments");

            migrationBuilder.DropIndex(
                name: "IX_InspectionComments_ConsultationId",
                table: "InspectionComments");

            migrationBuilder.AddColumn<Guid>(
                name: "RootCommentId",
                table: "Consultations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "RootCommentId1",
                table: "Consultations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_RootCommentId1",
                table: "Consultations",
                column: "RootCommentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_InspectionComments_RootCommentId1",
                table: "Consultations",
                column: "RootCommentId1",
                principalTable: "InspectionComments",
                principalColumn: "Id");
        }
    }
}
