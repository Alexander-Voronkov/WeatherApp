using Domain.Common.EntitiesAbstractions;

namespace Domain.UserAggregate;

public record UserId(int value) : BaseEntityId<int>(value);
