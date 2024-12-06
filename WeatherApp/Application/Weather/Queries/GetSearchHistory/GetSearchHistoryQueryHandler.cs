using Application.Common.Interfaces.Data;
using Application.Users;
using Dapper;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;

namespace Application.Weather.Queries.GetSearchHistory;
public class GetSearchHistoryQueryHandler : IRequestHandler<GetSearchHistoryQuery, SearchHistoryResult>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;
    private readonly IUserContextAccessor _userContextAccessor;

    public GetSearchHistoryQueryHandler(
        ISqlConnectionFactory sqlConnectionFactory,
        IUserContextAccessor userContextAccessor)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
        _userContextAccessor = userContextAccessor;
    }

    public async Task<SearchHistoryResult> Handle(GetSearchHistoryQuery request, CancellationToken cancellationToken)
    {
        var connection = await _sqlConnectionFactory.GetOpenConnection();

        const string sql =
            $"""
                SELECT 
                    wr."City",
                    wr."RequestedAt",
                    wr."RawJsonData"
                FROM "WeatherRequests" wr
                WHERE wr."UserId" = @userId
            """;

        var searchResults = await connection.QueryAsync<SearchHistoryDto>(sql,
            new
            {
                userId = _userContextAccessor.GetCurrentUserId().Value
            });

        var searchResult = new SearchHistoryResult(searchResults.ToArray());

        return searchResult;
    }
}