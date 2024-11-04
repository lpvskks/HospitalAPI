using System.ComponentModel.DataAnnotations;

namespace webNET_2024_aspnet_1.DBContext.DTO.DictionaryDTO
{
    public class SpecialityDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
