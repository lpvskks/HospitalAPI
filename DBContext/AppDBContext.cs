using Microsoft.EntityFrameworkCore;
using webNET_2024_aspnet_1.DBContext.Models;

namespace webNET_2024_aspnet_1.DBContext
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<BlackToken> BlackTokens { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Speciality> Specialties { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Speciality>().HasData(
                new Speciality { Id = Guid.Parse("e8f93a49-b93f-47f0-a912-08dbffad6d0e"), Name = "Акушер-гинеколог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123266"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("302d5c0c-5623-4810-a913-08dbffad6d0e"), Name = "Анестезиолог-реаниматолог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123261"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("2c4b19f5-511d-4f27-a914-08dbffad6d0e"), Name = "Дерматовенеролог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123256"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("4676b2f4-de54-4fce-a915-08dbffad6d0e"), Name = "Инфекционист", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123252"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("b0f1d7c7-18e5-488b-a916-08dbffad6d0e"), Name = "Кардиолог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123247"), DateTimeKind.Utc) }
            );

        }
    }
}
