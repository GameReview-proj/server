namespace GameReview.Data.Caching;

public interface ICachingService
{
    void Set(string key, string value);
    string Get(string key);
}