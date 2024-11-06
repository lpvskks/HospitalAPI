using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models.Enums;
using webNET_2024_aspnet_1.DBContext.Models;

namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class InspectionCreateDTO
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public string? Anamnesis { get; set; }
        [Required]
        public string? Complaints { get; set; }
        [Required]
        public string? Treatment { get; set; }
        [Required]
        public Conclusion Conclusion { get; set; }
        public DateTime? NextVisitDate { get; set; }
        public DateTime? DeathDate { get; set; }
        public Guid? PreviousInspectionId { get; set; }
        [Required]
        public List<DiagnosisCreateDTO> Diagnoses { get; set; }
        public List<ConsultationCreateDTO> Consultations { get; set; }
    }
}
