using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace webNET_2024_aspnet_1.Migrations
{
    /// <inheritdoc />
    public partial class InitIcdTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BlackTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Blacktoken = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlackTokens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    Phone = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IcdTens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ID = table.Column<int>(type: "integer", nullable: false),
                    REC_CODE = table.Column<string>(type: "text", nullable: true),
                    MKB_CODE = table.Column<string>(type: "text", nullable: true),
                    MKB_NAME = table.Column<string>(type: "text", nullable: true),
                    ID_PARENT = table.Column<int>(type: "integer", nullable: true),
                    ACTUAL = table.Column<int>(type: "integer", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IcdTens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Birthday = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialties", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Specialties",
                columns: new[] { "Id", "CreateTime", "Name" },
                values: new object[,]
                {
                    { new Guid("15e97e43-315c-44b5-a923-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3139), "Эндокринолог" },
                    { new Guid("2c4b19f5-511d-4f27-a914-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3256), "Дерматовенеролог" },
                    { new Guid("2e73cece-5fda-4211-a921-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3169), "Уролог" },
                    { new Guid("302d5c0c-5623-4810-a913-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3261), "Анестезиолог-реаниматолог" },
                    { new Guid("4676b2f4-de54-4fce-a915-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3252), "Инфекционист" },
                    { new Guid("5aa83ee6-9bb0-4afe-a91b-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3212), "Психиатр" },
                    { new Guid("6c20f45d-a7d1-4605-a91c-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3203), "Психолог" },
                    { new Guid("6cb7fe40-bafe-49bc-a917-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3237), "Невролог" },
                    { new Guid("75735935-74d3-4fa2-a918-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3232), "Онколог" },
                    { new Guid("87a9c38c-0d2d-4a52-a91a-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3217), "Офтальмолог" },
                    { new Guid("9ea305d2-b1f8-405e-a91f-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3183), "Терапевт" },
                    { new Guid("b0f1d7c7-18e5-488b-a916-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3247), "Кардиолог" },
                    { new Guid("bec96e6f-8673-47c9-a922-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3164), "Хирург" },
                    { new Guid("bf1f4b00-cf9c-48e4-a91e-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3188), "Стоматолог" },
                    { new Guid("d82c6890-d26d-450b-a920-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3178), "УЗИ-специалист" },
                    { new Guid("dfcc00ff-6595-41ad-a91d-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3198), "Рентгенолог" },
                    { new Guid("e8f93a49-b93f-47f0-a912-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3266), "Акушер-гинеколог" },
                    { new Guid("ed1b936e-9c67-4da6-a919-08dbffad6d0e"), new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3227), "Отоларинголог" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlackTokens");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "IcdTens");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Specialties");
        }
    }
}
