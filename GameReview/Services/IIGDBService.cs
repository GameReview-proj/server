using GameReview.Data.DTOs.IGDB;

namespace GameReview.Services;

public interface IIGDBService
{
    public IEnumerable<IGDBQueryResult<ExternalApiGame>> GetGames(string? name,
        List<string>? fields,
        int? from,
        int? take,
        List<int>? platforms,
        List<int>? genres);
    public IEnumerable<ExternalAPIGenre> GetGenres(List<string>? fields);
}