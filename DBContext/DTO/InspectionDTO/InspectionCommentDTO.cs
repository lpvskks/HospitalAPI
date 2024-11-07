using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models;

namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class InspectionCommentDTO
    {

        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public Guid? ParentId { get; set; }
        public string? Content { get; set; }
        public Doctor Author { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}
