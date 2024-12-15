using Microsoft.AspNetCore.Mvc;
using AkilliTarimUygulamasi.Services; // Servis katmanına erişim
using AkilliTarimUygulamasi.Models;  // Model erişimi

namespace AkilliTarimUygulamasi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FarmController : ControllerBase
    {
        private readonly IFarmDataService _farmDataService;

        // FarmDataService bağımlılığı enjekte ediliyor
        public FarmController(IFarmDataService farmDataService)
        {
            _farmDataService = farmDataService;
        }

        // Tarım verilerini almak
        [HttpGet]
        public IActionResult GetFarmData()
        {
            var farmData = _farmDataService.GetAllFarmData();
            if (farmData == null || !farmData.Any())
            {
                return NotFound("No farm data available.");
            }
            return Ok(farmData);
        }

        // Tarım verisi eklemek
        [HttpPost]
        public IActionResult AddFarmData([FromBody] FarmData farmData)
        {
            if (farmData == null)
            {
                return BadRequest("Farm data is invalid.");
            }
            _farmDataService.AddFarmData(farmData);
            return CreatedAtAction(nameof(GetFarmData), new { id = farmData.Id }, farmData);
        }
    }
}