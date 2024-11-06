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
    [Migration("20241106191718_AddTablesForInspections")]
    partial class AddTablesForInspections
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

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.Consultation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("CommentsNumber")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("InspectionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SpecialityId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("InspectionId");

                    b.HasIndex("SpecialityId");

                    b.ToTable("Consultations");
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.Diagnosis", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(5000)
                        .HasColumnType("character varying(5000)");

                    b.Property<Guid?>("InspectionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("InspectionId");

                    b.ToTable("Diagnoses");
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

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.IcdTen", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int?>("Actual")
                        .HasColumnType("integer")
                        .HasColumnName("ACTUAL");

                    b.Property<string>("Code")
                        .HasColumnType("text")
                        .HasColumnName("MKB_CODE");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Name")
                        .HasColumnType("text")
                        .HasColumnName("MKB_NAME");

                    b.Property<int?>("ParentId")
                        .HasColumnType("integer")
                        .HasColumnName("ID_PARENT");

                    b.Property<string>("RecordCode")
                        .HasColumnType("text")
                        .HasColumnName("REC_CODE");

                    b.Property<int>("UnicalId")
                        .HasColumnType("integer")
                        .HasColumnName("ID");

                    b.HasKey("Id");

                    b.ToTable("IcdTens");
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.Inspection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Anamnesis")
                        .HasColumnType("text");

                    b.Property<Guid?>("BaseInspectionId")
                        .HasColumnType("uuid");

                    b.Property<int>("Conclusion")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("DeathDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DoctorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("NextVisitDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("PatientId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("PreviousInspectionId")
                        .HasColumnType("uuid");

                    b.Property<string>("Treatment")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Inspections");
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.InspectionComment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ConsultationId")
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("ModifyTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("ConsultationId")
                        .IsUnique();

                    b.ToTable("InspectionComments");
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
                        },
                        new
                        {
                            Id = new Guid("6cb7fe40-bafe-49bc-a917-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3237),
                            Name = "Невролог"
                        },
                        new
                        {
                            Id = new Guid("75735935-74d3-4fa2-a918-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3232),
                            Name = "Онколог"
                        },
                        new
                        {
                            Id = new Guid("ed1b936e-9c67-4da6-a919-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3227),
                            Name = "Отоларинголог"
                        },
                        new
                        {
                            Id = new Guid("87a9c38c-0d2d-4a52-a91a-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3217),
                            Name = "Офтальмолог"
                        },
                        new
                        {
                            Id = new Guid("5aa83ee6-9bb0-4afe-a91b-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3212),
                            Name = "Психиатр"
                        },
                        new
                        {
                            Id = new Guid("6c20f45d-a7d1-4605-a91c-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3203),
                            Name = "Психолог"
                        },
                        new
                        {
                            Id = new Guid("dfcc00ff-6595-41ad-a91d-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3198),
                            Name = "Рентгенолог"
                        },
                        new
                        {
                            Id = new Guid("bf1f4b00-cf9c-48e4-a91e-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3188),
                            Name = "Стоматолог"
                        },
                        new
                        {
                            Id = new Guid("9ea305d2-b1f8-405e-a91f-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3183),
                            Name = "Терапевт"
                        },
                        new
                        {
                            Id = new Guid("d82c6890-d26d-450b-a920-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3178),
                            Name = "УЗИ-специалист"
                        },
                        new
                        {
                            Id = new Guid("2e73cece-5fda-4211-a921-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3169),
                            Name = "Уролог"
                        },
                        new
                        {
                            Id = new Guid("bec96e6f-8673-47c9-a922-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3164),
                            Name = "Хирург"
                        },
                        new
                        {
                            Id = new Guid("15e97e43-315c-44b5-a923-08dbffad6d0e"),
                            CreateTime = new DateTime(2023, 12, 18, 16, 40, 53, 12, DateTimeKind.Utc).AddTicks(3139),
                            Name = "Эндокринолог"
                        });
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.Consultation", b =>
                {
                    b.HasOne("webNET_2024_aspnet_1.DBContext.Models.Inspection", null)
                        .WithMany("Consultations")
                        .HasForeignKey("InspectionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webNET_2024_aspnet_1.DBContext.Models.Speciality", "Speciality")
                        .WithMany()
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Speciality");
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.Diagnosis", b =>
                {
                    b.HasOne("webNET_2024_aspnet_1.DBContext.Models.Inspection", null)
                        .WithMany("Diagnoses")
                        .HasForeignKey("InspectionId");
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.Inspection", b =>
                {
                    b.HasOne("webNET_2024_aspnet_1.DBContext.Models.Doctor", "Doctor")
                        .WithMany()
                        .HasForeignKey("DoctorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webNET_2024_aspnet_1.DBContext.Models.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.InspectionComment", b =>
                {
                    b.HasOne("webNET_2024_aspnet_1.DBContext.Models.Doctor", "Author")
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("webNET_2024_aspnet_1.DBContext.Models.Consultation", null)
                        .WithOne("RootComment")
                        .HasForeignKey("webNET_2024_aspnet_1.DBContext.Models.InspectionComment", "ConsultationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.Consultation", b =>
                {
                    b.Navigation("RootComment");
                });

            modelBuilder.Entity("webNET_2024_aspnet_1.DBContext.Models.Inspection", b =>
                {
                    b.Navigation("Consultations");

                    b.Navigation("Diagnoses");
                });
#pragma warning restore 612, 618
        }
    }
}
