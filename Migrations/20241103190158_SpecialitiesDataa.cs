using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webNET_2024_aspnet_1.Migrations
{
    /// <inheritdoc />
    public partial class SpecialitiesDataa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "CreateTime", "Name" },
                values: new object[,]
                {
                    { new Guid("2c4b19f5-511d-4f27-a914-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3256), "Дерматовенеролог" },
                    { new Guid("302d5c0c-5623-4810-a913-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3261), "Анестезиолог-реаниматолог" },
                    { new Guid("4676b2f4-de54-4fce-a915-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3252), "Инфекционист" },
                    { new Guid("b0f1d7c7-18e5-488b-a916-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3247), "Кардиолог" },
                    { new Guid("e8f93a49-b93f-47f0-a912-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3266), "Акушер-гинеколог" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("2c4b19f5-511d-4f27-a914-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("302d5c0c-5623-4810-a913-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("4676b2f4-de54-4fce-a915-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("b0f1d7c7-18e5-488b-a916-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("e8f93a49-b93f-47f0-a912-08dbffad6d0e"));
        }
    }
}
