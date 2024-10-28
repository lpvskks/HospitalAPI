using System.ComponentModel.DataAnnotations;

namespace webNET_2024_aspnet_1.DBContext.DTO.DoctorDTO
{
    public class LoginCredentialsDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
