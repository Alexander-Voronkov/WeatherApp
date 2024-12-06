using Application.Weather.Queries.GetWeatherByCity;

namespace Application.Services;
public interface IWeatherService
{
    Task<CityWeatherDto> GetWeatherByCity(string cityName);
}
