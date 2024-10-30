using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models.Enums;

namespace webNET_2024_aspnet_1.DBContext.DTO.PatientDTO
{
    public class PatientCreateDTO
    {
        [Required]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        [Required]
        public Gender Gender { get; set; }
    }
}
