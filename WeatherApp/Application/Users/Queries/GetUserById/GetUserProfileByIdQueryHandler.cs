using Dapper;
using Application.Common.Interfaces.Data;
using MediatR;

namespace Application.Users.Queries.GetUserById;
internal class GetUserProfileByIdQueryHandler : IRequestHandler<GetUserProfileByIdQuery, UserProfileDto>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public GetUserProfileByIdQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<UserProfileDto> Handle(GetUserProfileByIdQuery request, CancellationToken cancellationToken)
    {
        var connection = await _sqlConnectionFactory.GetOpenConnection();

        const string sql = 
            $"""   
                SELECT 
                    u."Email"
                FROM "Users" u
                WHERE u."Id" = @userId;
            """;

        var profile = await connection.QuerySingleAsync<UserProfileDto>(sql, new
        {
            userId = request.userId
        });

        return profile;
    }
}
