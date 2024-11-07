using Microsoft.EntityFrameworkCore;
using webNET_2024_aspnet_1.DBContext;
using webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO;
using webNET_2024_aspnet_1.DBContext.Models;
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
                throw new BadHttpRequestException("Patient not found!");
            }
            var doctor = await _dbContext.Doctors.FindAsync(doctorId);
            if (doctor == null)
            {
                throw new BadHttpRequestException("You are not doctor");
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
                NextVisitDate = inspectionCreateDTO.NextVisitDate,
                Patient = patient,
                Doctor = doctor,
                Diagnoses = new List<Diagnosis>(),
                Consultations = new List<Consultation>()
            };
            foreach (var diagnosisDTO in inspectionCreateDTO.Diagnoses)
            {
                var diagnos = await _dbContext.IcdTens.FirstOrDefaultAsync(d => d.Id == diagnosisDTO.IcdDiagnosisId);
                if (diagnos == null)
                {
                    throw new BadHttpRequestException("Diagnosis not found!");
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
                    throw new BadHttpRequestException("Speciality not found!");
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
                        ConsultationId = inspection.Id
                    };
                    await _dbContext.InspectionComments.AddAsync(inspectionComment);
                }
                Consultation consultation = new Consultation()
                {
                    Id = Guid.NewGuid(),
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
                throw new BadHttpRequestException("Inspection not found!");
            }

            var newDiagnoses = new List<Diagnosis>();

            foreach (var diagnosisDTO in inspectionEditDTO.Diagnoses)
            {
                var diagnos = await _dbContext.IcdTens.FirstOrDefaultAsync(d => d.Id == diagnosisDTO.IcdDiagnosisId);
                if (diagnos == null)
                {
                    throw new BadHttpRequestException("Diagnosis not found!");
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

    }
}
