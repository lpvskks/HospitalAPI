using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webNET_2024_aspnet_1.Additional_Services.TokenHelpers;
using webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO;
using webNET_2024_aspnet_1.DBContext.DTO.PatientDTO;
using webNET_2024_aspnet_1.DBContext.Models.Enums;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IInspectionService _inspectionService;
        private readonly TokenInteraction _tokenInteraction;
        public PatientController(IPatientService patientService, IInspectionService inspectionService, TokenInteraction tokenInteraction)
        {
            _patientService = patientService;
            _inspectionService = inspectionService;
            _tokenInteraction = tokenInteraction;
        }
        [HttpPost]
        [Authorize(Policy = "TokenBlackListPolicy")]
        public async Task<IActionResult> CreatePatient([FromBody] PatientCreateDTO patientCreateDTO)
        {
           
            return Ok(await _patientService.CreatePatient(patientCreateDTO));
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "TokenBlackListPolicy")]
        public async Task<IActionResult> GetPatientCard(Guid id)
        {
            return Ok(await _patientService.GetPatientCard(id));
        }

        [HttpPost("{id}/inspections")]
        [Authorize(Policy = "TokenBlackListPolicy")]
        public async Task<IActionResult> CreateInspection(Guid id, InspectionCreateDTO inspectionCreateDTO)
        {
            string token = _tokenInteraction.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedAccessException("Данный пользователь не авторизован");
            }
            var idString = _tokenInteraction.GetIdFromToken(token);
            Guid doctorId = Guid.Parse(idString);
            return Ok(await _inspectionService.CreateInspection(id, doctorId, inspectionCreateDTO));
        }

        [HttpGet("{id}/inspections/search")]
        [Authorize(Policy = "TokenBlackListPolicy")]
        public async Task<IActionResult> GetInpectionsWithoutNested(Guid id, string? request)
        {
            return Ok(await _patientService.GetInspectionsWithoutNested(id, request));
        }

        [HttpGet("")]
        [Authorize(Policy = "TokenBlackListPolicy")]
        public async Task<IActionResult> GetPatientPagedList(string? name, [FromQuery]List<Conclusion>? conslusions, Sorting? sorting, bool shudeledVisits, bool onlyMine, int page, int size)
        {
            string token = _tokenInteraction.GetTokenFromHeader();
            if (token == null)
            {
                throw new UnauthorizedAccessException("Данный пользователь не авторизован");
            }
            var idString = _tokenInteraction.GetIdFromToken(token);
            Guid doctorId = Guid.Parse(idString);
            return Ok(await _patientService.GetPatientPagedList(doctorId, name, conslusions, sorting, shudeledVisits, onlyMine, page, size));
        }
    }
}
