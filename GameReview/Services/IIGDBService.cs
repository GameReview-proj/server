using GameReview.Data.DTOs.IGDB;

namespace GameReview.Services;

public interface IIGDBService
{
    public IEnumerable<IGDBQueryResult<ExternalApiGame>> GetGamesByName(string name, List<string>? fields, int? from, int? take, List<int>? platforms);
    public IEnumerable<ExternalAPIGenre> GetGenres(List<string>? fields);
}