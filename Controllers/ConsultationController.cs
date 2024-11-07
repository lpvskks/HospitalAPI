using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webNET_2024_aspnet_1.Additional_Services.TokenHelpers;
using webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Controllers
{
    [Route("api/consultation")]
    [ApiController]
    public class ConsultationController : ControllerBase
    {
        private readonly IConsultationService _consultationService;
        private readonly TokenInteraction _tokenInteraction;
        public ConsultationController(IConsultationService consultationService, TokenInteraction tokenInteraction)
        {
            _consultationService = consultationService;
            _tokenInteraction = tokenInteraction;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetConcreteConsultation(Guid id)
        {
            return Ok(await _consultationService.GetConcreteConsultation(id));
        }

        [HttpPost("{id}/comment")]
        public async Task<IActionResult> CreateComment(Guid id, CreateConsultationCommentDTO commentDTO)
        {
            string token = _tokenInteraction.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedAccessException("Данный пользователь не авторизован");
            }
            var idString = _tokenInteraction.GetIdFromToken(token);
            Guid doctorId = Guid.Parse(idString);
            return Ok(await _consultationService.CreateComment(id, doctorId, commentDTO));
        }

        [HttpPut("{id}/comment")]
        public async Task<IActionResult> UpdateComment(Guid id, CommentDTO commentDTO)
        {
            string token = _tokenInteraction.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedAccessException("Данный пользователь не авторизован");
            }
            var idString = _tokenInteraction.GetIdFromToken(token);
            Guid doctorId = Guid.Parse(idString);
            await _consultationService.UpdateComment(id, doctorId, commentDTO);
            return Ok();
        }
    }
}
