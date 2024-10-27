using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models.Enums;

namespace webNET_2024_aspnet_1.DBContext.DTO
{
    public class DoctorRegisterDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public string Phone { get; set; }
    }
}
