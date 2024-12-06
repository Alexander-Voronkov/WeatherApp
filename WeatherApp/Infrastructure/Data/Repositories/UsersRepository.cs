using Application.Users;
using Domain.UserAggregate;

namespace Infrastructure.Data.Repositories;

internal class UsersRepository : BaseRepository<User, UserId, int>, IUsersRepository
{
    public UsersRepository(ApplicationDbContext context) : base(context)
    {
    }
}