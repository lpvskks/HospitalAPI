using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webNET_2024_aspnet_1.DBContext.DTO.PatientDTO;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientCreateDTO patientCreateDTO)
        {
           
            return Ok(await _patientService.CreatePatient(patientCreateDTO));
        }

    }
}
