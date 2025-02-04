﻿using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models.Enums;

namespace webNET_2024_aspnet_1.DBContext.DTO.DoctorDTO
{
    public class DoctorEditDTO
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        [Required]
        public Gender Gender { get; set; }
        public string Phone { get; set; }
    }
}
