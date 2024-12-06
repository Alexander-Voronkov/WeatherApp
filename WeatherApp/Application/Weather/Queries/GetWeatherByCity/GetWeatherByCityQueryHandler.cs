using Application.Services;
using Application.Users;
using Domain.WeatherAggregate;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace Application.Weather.Queries.GetWeatherByCity;
public class GetWeatherByCityQueryHandler : IRequestHandler<GetWeatherByCityQuery, CityWeatherDto>
{
    private readonly IWeatherService _weatherService;
    private readonly IDistributedCache _cache;
    private readonly IUserContextAccessor _userContext;
    private readonly IWeatherRequestsRepository _weatherRequestsRepository;

    public GetWeatherByCityQueryHandler(
        IWeatherService weatherService,
        IDistributedCache cache,
        IUserContextAccessor userContext,
        IWeatherRequestsRepository weatherRequestsRepo)
    {
        _weatherService = weatherService;
        _cache = cache;
        _userContext = userContext;
        _weatherRequestsRepository = weatherRequestsRepo;
    }

    public async Task<CityWeatherDto> Handle(GetWeatherByCityQuery request, CancellationToken cancellationToken)
    {
        CityWeatherDto cityWeatherDto;

        var cachedDto = await _cache.GetStringAsync(request.city.ToLower(), cancellationToken);

        if (cachedDto == null)
        {
            cityWeatherDto = await _weatherService.GetWeatherByCity(request.city);

            await _cache.SetStringAsync(request.city.ToLower(), JsonConvert.SerializeObject(cityWeatherDto), new DistributedCacheEntryOptions() { SlidingExpiration = TimeSpan.FromMinutes(10) });
        }
        else
        {
            cityWeatherDto = JsonConvert.DeserializeObject<CityWeatherDto>(cachedDto)!;
        }

        var weatherRequest = WeatherRequest.Create(request.city, JsonConvert.SerializeObject(cityWeatherDto), _userContext.GetCurrentUserId());

        await _weatherRequestsRepository.AddAsync(weatherRequest);

        return cityWeatherDto;
    }
}