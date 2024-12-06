using Application.Common.Interfaces.Data;
using Domain.UserAggregate;

namespace Application.Users;

public interface IUsersRepository : IRepository<User, UserId, int>
{
}