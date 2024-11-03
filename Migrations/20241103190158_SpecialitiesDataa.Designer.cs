﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using webNET_2024_aspnet_1.DBContext;

#nullable disable

namespace webNET_2024_aspnet_1.Migrations
{
    [DbContext(typeof(AppDBContext))]
    [Migration("20241103190158_SpecialitiesDataa")]
    partial class SpecialitiesDataa
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.BlackToken", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Blacktoken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("BlackTokens");
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.Speciality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Specialties");

                    b.HasData(
                        new
                        {
                            Id = new Guid("e8f93a49-b93f-47f0-a912-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3266),
                            Name = "Акушер-гинеколог"
                        },
                        new
                        {
                            Id = new Guid("302d5c0c-5623-4810-a913-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3261),
                            Name = "Анестезиолог-реаниматолог"
                        },
                        new
                        {
                            Id = new Guid("2c4b19f5-511d-4f27-a914-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3256),
                            Name = "Дерматовенеролог"
                        },
                        new
                        {
                            Id = new Guid("4676b2f4-de54-4fce-a915-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3252),
                            Name = "Инфекционист"
                        },
                        new
                        {
                            Id = new Guid("b0f1d7c7-18e5-488b-a916-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3247),
                            Name = "Кардиолог"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
