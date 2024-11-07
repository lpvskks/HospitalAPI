using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using webNET_2024_aspnet_1.DBContext.Models;

namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class GetConcreteConsultationDTO
    {
        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
        public Guid InspectionId { get; set; }
        public Speciality Speciality { get; set; }
        public List<ConsultationCommentDTO>? Comments { get; set; }
    }
}
