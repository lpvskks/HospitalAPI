using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webNET_2024_aspnet_1.DBContext.DTO.InspectionDTO;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Controllers
{
    [Route("api/inspection")]
    [ApiController]
    public class InspectionController : ControllerBase
    {
        private readonly IInspectionService _inspectionService;
        public InspectionController(IInspectionService inspectionService)
        {
            _inspectionService = inspectionService;
        }

        [HttpPut("{id}")]
        [Authorize(Policy = "TokenBlackListPolicy")]
        public async Task<IActionResult> EditInspection(Guid id, InspectionEditDTO inspectionEditDTO)
        {
            await _inspectionService.EditInspection(id, inspectionEditDTO);
            return Ok();
        }

        [HttpGet("{id}")]
        [Authorize(Policy = "TokenBlackListPolicy")]
        public async Task <IActionResult> GetConcreteInspection(Guid id)
        {
            return Ok(await _inspectionService.GetConcreteInspection(id));
        }

        [HttpGet("{id}/chain")]
        [Authorize(Policy = "TokenBlackListPolicy")]
        public async Task<IActionResult> GetNestedInpections(Guid id)
        {
            return Ok(await _inspectionService.GetNestedInspections(id));
        }
    }
}
