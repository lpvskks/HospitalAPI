using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webNET_2024_aspnet_1.DBContext.Models
{
    public class Consultation
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        public Guid InspectionId { get; set; }
        public Speciality Speciality { get; set; }
        public Guid? RootCommentId { get; set; }

        [ForeignKey("RootCommentId")]
        public InspectionComment? RootComment { get; set; }
        public int CommentsNumber { get; set; }
    }
}
