using Domain.UserAggregate.ValueObjects;

namespace Domain.Entities.DomainServices;

public interface IUserUniquenessHelper
{
    public Task<bool> IsUserUnique(
        UserEmail email);
}