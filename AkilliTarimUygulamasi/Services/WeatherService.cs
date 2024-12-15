using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AkilliTarimUygulamasi.Models;

public class WeatherService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;

    public WeatherService(HttpClient httpClient, string apiKey)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
    }

    public async Task<WeatherResponse> GetFormattedWeatherAsync(string locationKey)
    {
        var requestUrl = $"http://dataservice.accuweather.com/currentconditions/v1/{locationKey}?apikey={_apiKey}&language=tr&details=true";
        var response = await _httpClient.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"API Error: {response.StatusCode}, Content: {errorContent}");
        }

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var content = await response.Content.ReadAsStringAsync();
        var weatherData = JsonSerializer.Deserialize<List<WeatherResponse>>(content, options)?.FirstOrDefault();

        if (weatherData == null)
        {
            throw new Exception("Weather data not found.");
        }

        return weatherData;
    }


}