using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models;

namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class ConsultationCommentDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public Guid? ParentId { get; set; }
        public string? Content { get; set; }
        public Guid AuthorId { get; set; }
        public string Author { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
