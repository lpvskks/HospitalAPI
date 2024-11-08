using System.Collections;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using webNET_2024_aspnet_1.AdditionalServices.Exceptions;
using webNET_2024_aspnet_1.AdditionalServices.HashPassword;
using webNET_2024_aspnet_1.AdditionalServices.Validators;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.DBContext.DTO.DictionaryDTO;
using webNET_2024_aspnet_1.DBContext.DTO.DoctorDTO;
using webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO;
using webNET_2024_aspnet_1.DBContext.DTO.PageDTO;
using webNET_2024_aspnet_1.DBContext.DTO.PatientDTO;
using webNET_2024_aspnet_1.DBContext.Models;
using webNET_2024_aspnet_1.DBContext.Models.Enums;
using webNET_2024_aspnet_1.Services.IServices;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            if (!NameValidator.IsValidName(patientCreateDTO.Name))
            {
                throw new BadRequestException("Неправильный формат имени. Имя и фамилия должны начинаться с заглавной буквы. Допускаются только буквы и тире. Отчество является необязательным.");
            }
            if (!BirthdayValidator.ValidateBirthday(patientCreateDTO.Birthday))
            {
                throw new BadRequestException("Дата рождения должна быть в пределах 01.01.1900 и не позднее нынешнего времени.");
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

        public async Task<List<InpectionWithoutNestedDTO>> GetInspectionsWithoutNested(Guid patientId, string? request)
        {

            var inspectionsQuery = _dbContext.Inspections
                .Include(i => i.Diagnoses)
                .Include(i => i.Patient)
                .Where(i => !i.HasNested && i.Patient.Id == patientId);

            if (!string.IsNullOrWhiteSpace(request))
            {
                inspectionsQuery = inspectionsQuery.Where(i =>
                    i.Diagnoses.Any(d => d.Type == DiagnosisType.Main &&
                                         (d.Code.Contains(request) || d.Name.Contains(request))));
            }

            var inspections = await inspectionsQuery.ToListAsync();
            var inspectionDtos = new List<InpectionWithoutNestedDTO>();

            foreach (var inspection in inspections)
            {
                var mainDiagnosis = await _dbContext.Diagnoses
                    .Where(d => d.InspectionId == inspection.Id && d.Type == DiagnosisType.Main)
                    .FirstOrDefaultAsync();

                if (mainDiagnosis != null)
                {
                    var diagnosisDTO = new DiagnosisDTO
                    {
                        Id = mainDiagnosis.Id,
                        Name = mainDiagnosis.Name,
                        Code = mainDiagnosis.Code
                    };

                    inspectionDtos.Add(new InpectionWithoutNestedDTO
                    {
                        Id = inspection.Id,
                        CreateTime = inspection.CreateTime,
                        Date = inspection.Date,
                        Diagnosis = diagnosisDTO
                    });
                }
            }
            return inspectionDtos;
        }
        private static IQueryable<Patient> SortPatient(IQueryable<Patient> patients, Sorting? sorting)
        {
            switch (sorting)
            {
                case Sorting.NameAsc:
                    patients = patients.OrderBy(d => d.Name);
                    break;

                case Sorting.NameDesc:
                    patients = patients.OrderByDescending(d => d.Name);
                    break;
                case Sorting.CreateAsc:
                    patients = patients.OrderBy(d => d.CreateTime);
                    break;

                case Sorting.CreateDesc:
                    patients = patients.OrderByDescending(d => d.CreateTime);
                    break;
            }
            return patients;
        }
        public async Task<PatientPagedListDTO> GetPatientPagedList(Guid doctorId,string? name,List<Conclusion>? conclusions,
Sorting? sorting,
            bool scheduledVisits,
            bool onlyMine,
            int page,
            int size)
        {
            if (page <= 0) page = 1;
            if (size <= 0) size = 1;

            var patientsQuery = _dbContext.Patients.AsQueryable();

            if (onlyMine)
            {
                patientsQuery = patientsQuery.Where(p => _dbContext.Inspections
                    .Any(i => i.Patient.Id == p.Id && i.Doctor.Id == doctorId));
            }

            if (!string.IsNullOrEmpty(name))
            {
                var loweredName = name.ToLower();
                patientsQuery = patientsQuery.Where(p => p.Name.ToLower().Contains(loweredName));
            }

            if (conclusions != null && conclusions.Any())
            {
                patientsQuery = patientsQuery.Where(p => _dbContext.Inspections
                    .Any(i => i.Patient.Id == p.Id && conclusions.Contains(i.Conclusion)));
            }

            if (scheduledVisits)
            {
                patientsQuery = patientsQuery.Where(p => _dbContext.Inspections
                    .Any(i => i.Patient.Id == p.Id && i.NextVisitDate != null));
            }

            patientsQuery = SortPatient(patientsQuery, sorting);

            var totalCount = await patientsQuery.CountAsync();
            var totalPages = (int)Math.Ceiling((double)totalCount / size);

            var patients = await patientsQuery
                .Skip((page - 1) * size)
                .Take(size)
                .Select(p => new PatientDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    CreateTime = p.CreateTime,
                    Birthday = p.Birthday,
                    Gender = p.Gender,
                })
                .ToListAsync();

            return new PatientPagedListDTO
            {
                Patients = patients,
                Pagination = new PageInfoDTO
                {
                    Size = size,
                    Count = totalPages,
                    Current = page
                }
            };
        }

    }
}

