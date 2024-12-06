using Application.Services;
using Application.Weather.Queries.GetWeatherByCity;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace Infrastructure.Services;
public class WeatherService : IWeatherService
{
    private readonly HttpClient _client;

    public WeatherService(IHttpClientFactory httpClientFactory)
    {
        _client = httpClientFactory.CreateClient("WeatherAPIClient");
    }

    public async Task<CityWeatherDto?> GetWeatherByCity(string cityName)
    {
        var geoCodeResponse = await _client.GetAsync($"geo/1.0/direct?q={cityName}&limit=1");
        var geoCodes = await geoCodeResponse.Content.ReadFromJsonAsync<GeoCodeResponse[]>();
        
        if (geoCodes?.Length == 0)
        {
            return null;
        }

        var geoCode = geoCodes![0];

        var cityWeatherResponse = await _client.GetAsync($"data/2.5/weather?lat={geoCode.lat}&lon={geoCode.lon}&limit=1");
        var cityWeatherDto = await cityWeatherResponse.Content.ReadFromJsonAsync<CityWeatherDto>();

        return cityWeatherDto;
    }
}
