using Domain.UserAggregate;
using System.Runtime.CompilerServices;

namespace Application.Users.Queries.Authenticate;

public class AuthenticationResult
{
    public int UserId { get; }
    public bool IsFailed { get; }
};