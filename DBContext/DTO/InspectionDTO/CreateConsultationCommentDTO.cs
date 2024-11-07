using System.ComponentModel.DataAnnotations;

namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class CreateConsultationCommentDTO
    {
        [Required]
        public string Content { get; set; }
        public Guid? ParentId { get; set; }  
    }
}
