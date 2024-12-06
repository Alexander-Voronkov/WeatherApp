using Application.Users;
using Domain.WeatherAggregate;

namespace Infrastructure.Data.Repositories;
public class WeatherRequestsRepository : BaseRepository<WeatherRequest, WeatherRequestId, Guid>, IWeatherRequestsRepository
{
    public WeatherRequestsRepository(ApplicationDbContext context) : base(context)
    {
    }
}
