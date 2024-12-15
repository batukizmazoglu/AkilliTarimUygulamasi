using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    private readonly WeatherService _weatherService;

    public WeatherController(WeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    [HttpGet("{locationKey}")]
    public async Task<IActionResult> GetWeather(string locationKey)
    {
        try
        {
            var weatherData = await _weatherService.GetFormattedWeatherAsync(locationKey);
            return Ok(weatherData); // JSON formatında döndür
        }
        catch (HttpRequestException ex)
        {
            return StatusCode(500, $"Error fetching weather data: {ex.Message}");
        }
    }
}