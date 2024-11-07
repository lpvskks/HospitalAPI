using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models;

namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class ConsultationDTO
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public Guid InspectionId { get; set; }
        public Speciality Speciality { get; set; }
        public Guid? RootCommentId { get; set; }

        [ForeignKey("RootCommentId")]
        public InspectionCommentDTO? RootComment { get; set; }
        public int CommentsNumber { get; set; }
    }
}
