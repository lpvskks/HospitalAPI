using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models.Enums;
using webNET_2024_aspnet_1.DBContext.Models;
using webNET_2024_aspnet_1.DBContext.DTO.DoctorDTO;

namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class InspectionDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public DateTime Date { get; set; }
        public string? Anamnesis { get; set; }
        public string? Complaints { get; set; }
        public string? Treatment { get; set; }
        public Conclusion Conclusion { get; set; }
        public DateTime? NextVisitDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public Guid? BaseInspectionId { get; set; }
        public Guid? PreviousInspectionId { get; set; }
        public Patient Patient { get; set; }
        public AuthorDTO Doctor { get; set; }
        public List<DiagnosisDTO> Diagnoses { get; set; }
        public List<ConsultationDTO> Consultations { get; set; }
    }
}
