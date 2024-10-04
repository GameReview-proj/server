using GameReview.Data.DTOs.IGDB;

namespace GameReview.Services;

public interface IIGDBService
{
    public IEnumerable<ExternalApiGame> GetGamesByName(string name, List<string> fields);
}