using MediatR;

namespace Application.Weather.Queries.GetWeatherByCity;
public record GetWeatherByCityQuery(string city) : IRequest<CityWeatherDto>;