using Microsoft.Extensions.Caching.Distributed;

namespace GameReview.Data.Caching.Impl;

public class CachingService : ICachingService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;

    public CachingService(IDistributedCache cache)
    {
        _cache = cache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
            SlidingExpiration = TimeSpan.FromMinutes(30)
        };
    }

    public string Get(string key)
    {
        return _cache.GetString(key);
    }

    public void Set(string key, string value)
    {
        _cache.SetString(key, value, _options);
    }
}
