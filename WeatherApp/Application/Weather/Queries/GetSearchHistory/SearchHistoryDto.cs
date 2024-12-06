namespace Application.Weather.Queries.GetSearchHistory;
public class SearchHistoryDto 
{
    public string? City { get; } 

    public DateTimeOffset RequestedAt { get; }
    
    public string? RawJsonData { get; }
};