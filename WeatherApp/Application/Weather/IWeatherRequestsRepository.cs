using Application.Common.Interfaces.Data;
using Domain.WeatherAggregate;

namespace Application.Users;

public interface IWeatherRequestsRepository : IRepository<WeatherRequest, WeatherRequestId, Guid>
{
}
