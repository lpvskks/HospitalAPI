using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models.Enums;

namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class DiagnosisDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        [Required]
        [MinLength(1)]
        public string Code { get; set; }
        [Required]
        [MinLength(1)]
        public string Name { get; set; }

        [MaxLength(5000)]
        public string? Description { get; set; }
        [Required]
        public DiagnosisType Type { get; set; }
    }
}
