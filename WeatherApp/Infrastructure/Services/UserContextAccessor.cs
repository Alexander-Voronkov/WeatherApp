using Application.Users;
using Domain.UserAggregate;
using IdentityModel;
using IdentityServer4.Extensions;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;
public class UserContextAccessor : IUserContextAccessor
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContextAccessor(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public UserId GetCurrentUserId()
    {
        return new UserId(int.Parse(_httpContextAccessor.HttpContext.User.Claims.First(x => x.Type == "userid").Value)); // bulletproof 
    }
}
