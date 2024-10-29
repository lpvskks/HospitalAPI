using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webNET_2024_aspnet_1.Additional_Services.TokenHelpers;
using webNET_2024_aspnet_1.DBContext.DTO.DoctorDTO;
using webNET_2024_aspnet_1.Services.IServices;

namespace webNET_2024_aspnet_1.Controllers
{
    [Route("api/doctor")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly TokenInteraction _tokenInteraction;
        public DoctorController(IDoctorService doctorService, TokenInteraction tokenInteraction)
        {
            _doctorService = doctorService;
            _tokenInteraction = tokenInteraction;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] DoctorRegisterDTO doctorRegisterDTO)
        {

            var tokenResponse = await _doctorService.Register(doctorRegisterDTO);
            return Ok(tokenResponse);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentialsDTO loginCredentialsDTO)
        {
            var tokenResponse = await _doctorService.Login(loginCredentialsDTO);
            return Ok(tokenResponse); 
        }


        [Authorize(Policy = "TokenBlackListPolicy")]
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            string token = _tokenInteraction.GetTokenFromHeader();

            await _doctorService.Logout(token);
            return Ok();

        }
    }
}
