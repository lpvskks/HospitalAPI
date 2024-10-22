using System.ComponentModel.DataAnnotations;
using Microsoft.IdentityModel.Abstractions;
using webNET_2024_aspnet_1.DBContext.Models.Enums;

namespace webNET_2024_aspnet_1.DBContext.Models
{
    public class Doctor
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
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
