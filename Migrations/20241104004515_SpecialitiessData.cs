using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webNET_2024_aspnet_1.Migrations
{
    /// <inheritdoc />
    public partial class SpecialitiessData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "CreateTime", "Name" },
                values: new object[,]
                {
                    { new Guid("15e97e43-315c-44b5-a923-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3139), "Эндокринолог" },
                    { new Guid("2e73cece-5fda-4211-a921-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3169), "Уролог" },
                    { new Guid("5aa83ee6-9bb0-4afe-a91b-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3212), "Психиатр" },
                    { new Guid("6c20f45d-a7d1-4605-a91c-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3203), "Психолог" },
                    { new Guid("6cb7fe40-bafe-49bc-a917-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3237), "Невролог" },
                    { new Guid("75735935-74d3-4fa2-a918-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3232), "Онколог" },
                    { new Guid("87a9c38c-0d2d-4a52-a91a-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3217), "Офтальмолог" },
                    { new Guid("9ea305d2-b1f8-405e-a91f-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3183), "Терапевт" },
                    { new Guid("bec96e6f-8673-47c9-a922-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3164), "Хирург" },
                    { new Guid("bf1f4b00-cf9c-48e4-a91e-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3188), "Стоматолог" },
                    { new Guid("d82c6890-d26d-450b-a920-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3178), "УЗИ-специалист" },
                    { new Guid("dfcc00ff-6595-41ad-a91d-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3198), "Рентгенолог" },
                    { new Guid("ed1b936e-9c67-4da6-a919-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3227), "Отоларинголог" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("15e97e43-315c-44b5-a923-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("2e73cece-5fda-4211-a921-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("5aa83ee6-9bb0-4afe-a91b-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("6c20f45d-a7d1-4605-a91c-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("6cb7fe40-bafe-49bc-a917-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("75735935-74d3-4fa2-a918-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("87a9c38c-0d2d-4a52-a91a-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("9ea305d2-b1f8-405e-a91f-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("bec96e6f-8673-47c9-a922-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("bf1f4b00-cf9c-48e4-a91e-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("d82c6890-d26d-450b-a920-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("dfcc00ff-6595-41ad-a91d-08dbffad6d0e"));

            migrationBuilder.DeleteData(
                table: "Specialties",
                keyColumn: "Id",
                keyValue: new Guid("ed1b936e-9c67-4da6-a919-08dbffad6d0e"));
        }
    }
}
