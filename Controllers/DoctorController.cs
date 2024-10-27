using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webNET_2024_aspnet_1.DBContext.DTO;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] DoctorRegisterDTO doctorRegisterDTO)
        {

            var tokenResponse = await _doctorService.Register(doctorRegisterDTO);
            return Ok(tokenResponse);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentialsDTO loginCredentialsDTO)
        {
            var tokenResponse = await _doctorService.Login(loginCredentialsDTO);
            return Ok(tokenResponse); 
        }

    }
}
