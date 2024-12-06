using MediatR;

namespace Application.Users.Queries.Authenticate;
public record AuthenticateQuery(string login, string pass) : IRequest<AuthenticationResult>;