using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace webNET_2024_aspnet_1.Migrations
{
    /// <inheritdoc />
    public partial class AddCommentIdToConsultion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_RootCommentId",
                table: "Consultations",
                column: "RootCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_InspectionComments_RootCommentId",
                table: "Consultations",
                column: "RootCommentId",
                principalTable: "InspectionComments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_InspectionComments_RootCommentId",
                table: "Consultations");

            migrationBuilder.DropIndex(
                name: "IX_Consultations_RootCommentId",
                table: "Consultations");

            migrationBuilder.DropColumn(
                name: "RootCommentId",
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
    }
}
