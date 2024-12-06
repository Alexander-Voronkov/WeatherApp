using Domain.Common.EntitiesAbstractions;

namespace Domain.WeatherAggregate;

public record WeatherRequestId(Guid value) : BaseEntityId<Guid>(value);
