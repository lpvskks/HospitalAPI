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
                new Speciality { Id = Guid.Parse("b0f1d7c7-18e5-488b-a916-08dbffad6d0e"), Name = "Кардиолог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123247"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("6cb7fe40-bafe-49bc-a917-08dbffad6d0e"), Name = "Невролог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123237"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("75735935-74d3-4fa2-a918-08dbffad6d0e"), Name = "Онколог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123232"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("ed1b936e-9c67-4da6-a919-08dbffad6d0e"), Name = "Отоларинголог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123227"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("87a9c38c-0d2d-4a52-a91a-08dbffad6d0e"), Name = "Офтальмолог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123217"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("5aa83ee6-9bb0-4afe-a91b-08dbffad6d0e"), Name = "Психиатр", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123212"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("6c20f45d-a7d1-4605-a91c-08dbffad6d0e"), Name = "Психолог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123203"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("dfcc00ff-6595-41ad-a91d-08dbffad6d0e"), Name = "Рентгенолог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123198"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("bf1f4b00-cf9c-48e4-a91e-08dbffad6d0e"), Name = "Стоматолог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123188"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("9ea305d2-b1f8-405e-a91f-08dbffad6d0e"), Name = "Терапевт", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123183"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("d82c6890-d26d-450b-a920-08dbffad6d0e"), Name = "УЗИ-специалист", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123178"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("2e73cece-5fda-4211-a921-08dbffad6d0e"), Name = "Уролог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123169"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("bec96e6f-8673-47c9-a922-08dbffad6d0e"), Name = "Хирург", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123164"), DateTimeKind.Utc) },
                new Speciality { Id = Guid.Parse("15e97e43-315c-44b5-a923-08dbffad6d0e"), Name = "Эндокринолог", CreateTime = DateTime.SpecifyKind(DateTime.Parse("2023-12-18T16:40:53.0123139"), DateTimeKind.Utc) }
            );

        }
    }
}
