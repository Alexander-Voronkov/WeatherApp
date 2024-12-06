using MediatR;

namespace Application.Weather.Queries.GetSearchHistory;
public record GetSearchHistoryQuery : IRequest<SearchHistoryResult>;
