using webNET_2024_aspnet_1.DBContext.Models;

namespace webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO
{
    public class ConsultationCreateDTO
    {
        public Guid SpecialityId { get; set; }

        public CommentDTO Comment { get; set; }
    }
}
