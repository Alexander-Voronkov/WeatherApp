using Dapper;
using Application.Common.Interfaces.Data;
using Domain.UserAggregate.ValueObjects;
using MediatR;

namespace Application.Users.Queries.Authenticate;
public class AuthenticateQueryHandler : IRequestHandler<AuthenticateQuery, AuthenticationResult>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public AuthenticateQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<AuthenticationResult> Handle(
        AuthenticateQuery query, 
        CancellationToken cancellationToken)
    {
        var connection = await _sqlConnectionFactory.GetOpenConnection();

        var pass = UserPassword.CreatePassword(query.pass);
        var email = UserEmail.CreateUserEmail(query.login);

        const string sql = 
            $"""
                SELECT 
                    u."Id" AS UserId,
                    0 as IsFailed
                FROM "Users" u
                WHERE u."Email" = @email AND u."PasswordHash" = @password;
            """;

        var authResult = await connection.QuerySingleAsync<AuthenticationResult>(sql, new
        {
            email = email.Value,
            password = pass.Value,
        });

        return authResult;
    }
}