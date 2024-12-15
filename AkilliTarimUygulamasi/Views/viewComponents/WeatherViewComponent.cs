// ViewComponents/WeatherViewComponent.cs

using Microsoft.AspNetCore.Mvc;

public class WeatherViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int locationId)
    {
        var weatherData = await GetWeatherData(locationId);
        return View(weatherData);
    }

    private async Task<WeatherData> GetWeatherData(int locationId)
    {
        // API çağrısı simülasyonu
        return new WeatherData 
        { 
            Temperature = 25,
            Humidity = 65,
            Forecast = "Güneşli"
        };
    }
}

public class WeatherData
{
    public int Temperature { get; set; }
    public int Humidity { get; set; }
    public string Forecast { get; set; }
}