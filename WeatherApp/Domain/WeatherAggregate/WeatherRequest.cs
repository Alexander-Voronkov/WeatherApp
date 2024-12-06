using Domain.Common.EntitiesAbstractions;
using Domain.UserAggregate;

namespace Domain.WeatherAggregate;

public class WeatherRequest : BaseEntity<WeatherRequestId, Guid>, IAggregateRoot
{
    private string _city;

    private DateTimeOffset _requestedAt;

    private UserId _userId;

    private string _rawJsonResponse;

    private WeatherRequest(
        string city,
        string rawJsonResponse,
        UserId requestActorId)
    {
        Id = new(Guid.NewGuid());
        _city = city;
        _requestedAt = DateTimeOffset.UtcNow;
        _rawJsonResponse = rawJsonResponse;
        _userId = requestActorId;
    }

    private WeatherRequest()
    {
        //ef
    }

    public static WeatherRequest Create(
        string city,
        string rawJsonResponse,
        UserId requestActorId)
    {
        return new WeatherRequest(city, rawJsonResponse, requestActorId);
    }
}