﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webNET_2024_aspnet_1.Services.IServices;
using webNET_2024_aspnet_1.DBContext.DTO.DictionaryDTO;

namespace webNET_2024_aspnet_1.Controllers
{
    [Route("api/dictionary")]
    [ApiController]
    public class DictionaryController : ControllerBase
    {
        private readonly IDictionaryService _dictionaryService;
        
        public DictionaryController(IDictionaryService dictionaryService)
        {
            _dictionaryService = dictionaryService;
        }

        [HttpGet("specialities")]
        public async Task<IActionResult> GetSpecialities(string name = null, int page = 1, int size = 10)
        {
            var specialitiesPagesListDTO = await _dictionaryService.GetPaginatedSpecialities(name, page, size);
            return Ok(specialitiesPagesListDTO);
        }
    }
}
