using System.ComponentModel.DataAnnotations;

namespace webNET_2024_aspnet_1.DBContext.Models
{
    public class InspectionComment
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public Guid? ParentId { get; set; }
        public string? Content { get; set; }
        public Doctor Author { get; set; }
        public DateTime ModifyTime { get; set; }
        public Guid ConsultationId { get; set; }  
    }
}
