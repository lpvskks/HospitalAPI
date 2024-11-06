using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models.Enums;

namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class DiagnosisCreateDTO
    {
        [Required]
        public Guid IcdDiagnosisId { get; set; }
        [MaxLength(5000)]
        public string? Description { get; set; }
        [Required]
        public DiagnosisType Type { get; set; }
    }
}
