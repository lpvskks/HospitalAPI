using Microsoft.EntityFrameworkCore;
using webNET_2024_aspnet_1.AdditionalServices.Exceptions;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.DBContext.DTO.DoctorDTO;
using webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO;
using webNET_2024_aspnet_1.DBContext.Models;
using webNET_2024_aspnet_1.DBContext.Models.Enums;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Services
{
    public class InspectionService : IInspectionService
    {
        private readonly AppDBContext _dbContext;

        public InspectionService(AppDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Guid> CreateInspection(Guid patientId, Guid doctorId, InspectionCreateDTO inspectionCreateDTO)
        {
            var patient = await _dbContext.Patients.FindAsync(patientId);
            if (patient == null)
            {
                throw new NotFoundException("Пациент не найден.");
            }
            var doctor = await _dbContext.Doctors.FindAsync(doctorId);
            if (doctor == null)
            {
                throw new ForbiddenException("Вы не доктор.");
            }
            Inspection inspection = new Inspection()
            {
                Id = Guid.NewGuid(),
                CreateTime = DateTime.UtcNow,
                Date = inspectionCreateDTO.Date,
                Anamnesis = inspectionCreateDTO.Anamnesis,
                Complaints = inspectionCreateDTO.Complaints,
                Treatment = inspectionCreateDTO.Treatment,
                Conclusion = inspectionCreateDTO.Conclusion,
                NextVisitDate = inspectionCreateDTO.NextVisitDate ?? null,
                Patient = patient,
                Doctor = doctor,
                Diagnoses = new List<Diagnosis>(),
                Consultations = new List<Consultation>()
            };
            Boolean hasPreviousId = inspectionCreateDTO.PreviousInspectionId != null;
            if (hasPreviousId)
            {
                var rootInspection = await _dbContext.Inspections.FirstOrDefaultAsync(i => i.Id == inspectionCreateDTO.PreviousInspectionId);
                if (rootInspection == null)
                {
                    throw new NotFoundException("Такого корневого элемента не существует!");
                }
                if (rootInspection.HasNested)
                {
                    throw new BadRequestException("У этого осмотра уже есть потомок!");
                }
                rootInspection.HasNested = true;
                if(rootInspection.PreviousInspectionId == null)
                {
                    inspection.BaseInspectionId = rootInspection.Id;
                    rootInspection.HasChain = true;
                }
                else { inspection.BaseInspectionId = rootInspection.BaseInspectionId; }
                inspection.PreviousInspectionId = rootInspection.Id;
            }
            foreach (var diagnosisDTO in inspectionCreateDTO.Diagnoses)
            {
                var diagnos = await _dbContext.IcdTens.FirstOrDefaultAsync(d => d.Id == diagnosisDTO.IcdDiagnosisId);
                if (diagnos == null)
                {
                    throw new NotFoundException("Диагноз не найден.");
                }
                Diagnosis diagnosis = new Diagnosis()
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.UtcNow,
                    Code = diagnos.Code,
                    Name = diagnos.Name,
                    Description = diagnosisDTO.Description,
                    Type = diagnosisDTO.Type
                };
                inspection.Diagnoses.Add(diagnosis);
            }
            foreach (var consultationDTO in inspectionCreateDTO.Consultations)
            {
                var speciality = await _dbContext.Specialties.FirstOrDefaultAsync(d => d.Id == consultationDTO.SpecialityId);
                if (speciality == null)
                {
                    throw new NotFoundException("Специальность не найдена.");
                }

                bool isCommented = !string.IsNullOrEmpty(consultationDTO.Comment?.Content);

                InspectionComment? inspectionComment = null;
                if (isCommented)
                {
                    inspectionComment = new InspectionComment()
                    {
                        Id = Guid.NewGuid(),
                        CreateTime = DateTime.UtcNow,
                        Content = consultationDTO.Comment.Content,
                        Author = doctor,
                        ModifyTime = DateTime.UtcNow,
                        ConsultationId = Guid.NewGuid()
                    };
                    _dbContext.InspectionComments.Add(inspectionComment);
                }
                Consultation consultation = new Consultation()
                {
                    Id = isCommented ? inspectionComment.ConsultationId : Guid.NewGuid(),
                    CreateTime = DateTime.UtcNow,
                    Speciality = speciality,
                    InspectionId = inspection.Id,
                    RootComment = inspectionComment,
                    RootCommentId = isCommented ? inspectionComment.Id : null,
                    CommentsNumber = isCommented ? 1 : 0
                };
                inspection.Consultations.Add(consultation);
            }
            _dbContext.Inspections.Add(inspection);
            await _dbContext.SaveChangesAsync();
            return inspection.Id;
        }

        public async Task EditInspection(Guid inspectionId, InspectionEditDTO inspectionEditDTO)
        {
            var inspection = await _dbContext.Inspections
                .Include(i => i.Diagnoses) 
                .FirstOrDefaultAsync(i => i.Id == inspectionId);

            if (inspection == null)
            {
                throw new NotFoundException("Осмотр не найден.");
            }

            var newDiagnoses = new List<Diagnosis>();

            bool hasMainDiagnosis = newDiagnoses.Any(d => d.Type == DiagnosisType.Main);

            foreach (var diagnosisDTO in inspectionEditDTO.Diagnoses)
            {
                if (diagnosisDTO.Type == DiagnosisType.Main && hasMainDiagnosis)
                {
                    throw new BadRequestException("Нельзя добавить более одного основного диагноза.");
                }

                var diagnos = await _dbContext.IcdTens.FirstOrDefaultAsync(d => d.Id == diagnosisDTO.IcdDiagnosisId);
                if (diagnos == null)
                {
                    throw new NotFoundException("Диагноз не найден.");
                }

                Diagnosis diagnosis = new Diagnosis()
                {
                    Id = Guid.NewGuid(),
                    CreateTime = DateTime.UtcNow,
                    Code = diagnos.Code,
                    Name = diagnos.Name,
                    Description = diagnosisDTO.Description,
                    Type = diagnosisDTO.Type
                };

                newDiagnoses.Add(diagnosis);
                if (diagnosis.Type == DiagnosisType.Main)
                {
                    hasMainDiagnosis = true;
                }

                _dbContext.Diagnoses.Add(diagnosis);
            }
            _dbContext.Diagnoses.RemoveRange(inspection.Diagnoses);

            inspection.Anamnesis = inspectionEditDTO.Anamnesis;
            inspection.Complaints = inspectionEditDTO.Complaints;
            inspection.Treatment = inspectionEditDTO.Treatment;
            inspection.Conclusion = inspectionEditDTO.Conclusion;
            inspection.NextVisitDate = inspectionEditDTO.NextVisitDate;
            inspection.DeathDate = inspectionEditDTO.DeathDate;
            inspection.Diagnoses = newDiagnoses; 

            await _dbContext.SaveChangesAsync();
        }

        public async Task<InspectionDTO> GetConcreteInspection(Guid inspectionId)
        {
            var inspection = await _dbContext.Inspections
                .Include(i => i.Patient)
                .Include(i => i.Doctor)
                .Include(i => i.Diagnoses)
                .Include(i => i.Consultations)
                    .ThenInclude(c => c.RootComment)
                .Include(i => i.Consultations)
                .ThenInclude(c => c.Speciality)
                .FirstOrDefaultAsync(i => i.Id == inspectionId);

            if (inspection == null)
            {
                throw new NotFoundException("Такого осмотра не сущетсвует!");
            }

            var inspectionDTO = new InspectionDTO
            {
                Id = inspection.Id,
                CreateTime = inspection.CreateTime,
                Date = inspection.Date,
                Anamnesis = inspection.Anamnesis ?? null,
                Complaints = inspection.Complaints ?? null,
                Treatment = inspection.Treatment ?? null,
                Conclusion = inspection.Conclusion,
                NextVisitDate = inspection.NextVisitDate,
                DeathDate = inspection.DeathDate,
                BaseInspectionId = inspection.BaseInspectionId,
                PreviousInspectionId = inspection.PreviousInspectionId,
                Patient = inspection.Patient ?? null,
                Doctor = new AuthorDTO
                {
                    Id = inspection.Doctor.Id,
                    CreateTime = inspection.Doctor.CreateTime,
                    Name = inspection.Doctor.Name,
                    Birthday = inspection.Doctor.Birthday,
                    Gender = inspection.Doctor.Gender,
                    Email = inspection.Doctor.Email,
                    Phone = inspection.Doctor.Phone
                },
                Diagnoses = inspection.Diagnoses?.Select(d => new DiagnosisDTO
                {
                    Id = d.Id,
                    CreateTime = d.CreateTime,
                    Code = d.Code ?? string.Empty,
                    Name = d.Name ?? string.Empty,
                    Description = d.Description,
                    Type = d.Type
                }).ToList(),
                Consultations = inspection.Consultations != null && inspection.Consultations.Any()
                    ? inspection.Consultations.Select(c => new ConsultationDTO
                    {
                        Id = c.Id,
                        CreateTime = c.CreateTime,
                        InspectionId = c.InspectionId,
                        Speciality = c.Speciality ?? null,
                        RootCommentId = c.RootCommentId,
                        RootComment = c.RootComment != null ? new InspectionCommentDTO
                        {
                            Content = c.RootComment.Content ?? null,
                            Author = new AuthorDTO
                            {
                                Id = c.RootComment.Author.Id,
                                CreateTime = c.RootComment.Author.CreateTime,
                                Name = c.RootComment.Author.Name,
                                Birthday = c.RootComment.Author.Birthday,
                                Gender = c.RootComment.Author.Gender,
                                Email = c.RootComment.Author.Email,
                                Phone = c.RootComment.Author.Phone
                            } ?? null,
                            CreateTime = c.RootComment.CreateTime,
                            Id = c.RootComment.Id,
                            ParentId = c.RootComment.ParentId ?? null,
                            ModifyTime = c.RootComment.ModifyTime,
                        } : null,
                        CommentsNumber = c.CommentsNumber
                    }).ToList()
                    : null
            };

            return inspectionDTO;
        }

        public async Task<List<NestedInspectionDTO>> GetNestedInspections(Guid inspectionId)
        {
            var rootInspection = await _dbContext.Inspections.FirstOrDefaultAsync(i => i.Id == inspectionId && i.HasChain);
            if (rootInspection == null)
            {
                throw new BadRequestException("Эта инспекция не корневая!");
            }
            var inspections = await _dbContext.Inspections
                   .Include(i => i.Doctor)
                   .Include(i => i.Patient)
                   .Where(i => i.BaseInspectionId == inspectionId)
                   .Select(i => new
                   {
                       Inspection = i,
                       MainDiagnosis = i.Diagnoses.FirstOrDefault(d => d.Type == DiagnosisType.Main)
                   })
                   .ToListAsync();

            var nestedInspections = new List<NestedInspectionDTO>();

            foreach (var item in inspections)
            {
                var nestedInspectionDTO = new NestedInspectionDTO
                {
                    Id = item.Inspection.Id,
                    CreateTime = item.Inspection.CreateTime,
                    PreviousId = item.Inspection.PreviousInspectionId ?? Guid.Empty,
                    Date = item.Inspection.Date,
                    Conclusion = item.Inspection.Conclusion,
                    DoctorId = item.Inspection.Doctor.Id,
                    Doctor = item.Inspection.Doctor.Name,
                    PatientId = item.Inspection.Patient.Id,
                    Patient = item.Inspection.Patient.Name,
                    Diagnosis = item.MainDiagnosis != null ? new DiagnosisDTO
                    {
                        Id = item.MainDiagnosis.Id,
                        Name = item.MainDiagnosis.Name,
                        Code = item.MainDiagnosis.Code
                    } : null,
                    HasNested = item.Inspection.HasNested,
                    HasChain = item.Inspection.HasChain
                };

                nestedInspections.Add(nestedInspectionDTO);
            }

            return nestedInspections;
        }
    }
}
