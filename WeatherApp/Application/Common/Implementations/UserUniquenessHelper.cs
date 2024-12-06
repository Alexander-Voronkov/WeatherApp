using Dapper;
using Application.Common.Interfaces.Data;
using Domain.Entities.DomainServices;
using Domain.UserAggregate.ValueObjects;

namespace Application.Common.Implementations;
internal class UserUniquenessHelper : IUserUniquenessHelper
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public UserUniquenessHelper(
        ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<bool> IsUserUnique(
        UserEmail email)
    {
        var connection = await _sqlConnectionFactory.GetOpenConnection();

        const string sql = 
            $"""
               SELECT CASE 
                    WHEN NOT EXISTS (
                        SELECT 1 
                        FROM "Users"
                        WHERE "Email" = @email
                    ) THEN 1
                    ELSE 0
                END;
            """;

        var uniquenessResult = await connection.QuerySingleAsync<bool>(sql, new
        {
            email = email.Value,
        });

        return uniquenessResult;
    }
}