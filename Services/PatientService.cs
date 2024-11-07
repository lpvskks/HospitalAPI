using Microsoft.EntityFrameworkCore;
using webNET_2024_aspnet_1.AdditionalServices.Exceptions;
using webNET_2024_aspnet_1.AdditionalServices.HashPassword;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.DBContext.DTO.DoctorDTO;
using webNET_2024_aspnet_1.DBContext.DTO.PatientDTO;
using webNET_2024_aspnet_1.DBContext.Models;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Services
{
    public class PatientService : IPatientService
    {
        private readonly AppDBContext _dbContext;
        public PatientService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool IsUniquePatient(string name)
        {
            var patient = _dbContext.Patients.FirstOrDefault(x => x.Name == name);
            if (patient == null)
            {
                return true;
            }
            return false;
        }
        public async Task<Guid> CreatePatient(PatientCreateDTO patientCreateDTO)
        {
            if (!IsUniquePatient(patientCreateDTO.Name))
            {
                throw new BadRequestException("Такой пациент уже существует");
            }
            Patient patient = new Patient()
            {
                Id = Guid.NewGuid(),
                Name = patientCreateDTO.Name,
                Birthday = patientCreateDTO.Birthday,
                Gender = patientCreateDTO.Gender,
                CreateTime = DateTime.UtcNow
            };

            await _dbContext.AddAsync(patient);
            await _dbContext.SaveChangesAsync();
            return patient.Id;
        }

        public async Task<PatientDTO> GetPatientCard(Guid id)
        {
            var patient = await _dbContext.Patients.FirstOrDefaultAsync(p => p.Id == id);
            if (patient != null)
            {
                return new PatientDTO
                {
                    Id = patient.Id,
                    Name = patient.Name,
                    Birthday = patient.Birthday,
                    Gender = patient.Gender,
                    CreateTime = DateTime.UtcNow
                };
            }
            else
            {
                throw new NotFoundException("Такого пациента не существует.");
            }
        }
    }
}

