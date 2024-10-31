using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models.Enums;

namespace webNET_2024_aspnet_1.DBContext.DTO.PatientDTO
{
    public class PatientDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
