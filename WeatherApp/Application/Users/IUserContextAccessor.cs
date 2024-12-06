using Domain.UserAggregate;

namespace Application.Users;

public interface IUserContextAccessor
{
    public UserId GetCurrentUserId();
}