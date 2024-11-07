using Microsoft.AspNetCore.Mvc;
using webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO;

namespace webNET_2024_aspnet_1.Services.IServices
{
    public interface IConsultationService
    {
        public Task<GetConcreteConsultationDTO> GetConcreteConsultation(Guid consultationId);
        public Task<Guid> CreateComment(Guid consultationId,Guid authorId, CreateConsultationCommentDTO commentDTO);
        public Task UpdateComment(Guid commentId, Guid doctorId, CommentDTO commentDTO);
    }
}
