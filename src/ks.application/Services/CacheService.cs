using ks.application.Services.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace ks.application.Services;
public class CacheService : ICacheService
{
    private readonly IDistributedCache distributedCache;
    public CacheService(IDistributedCache distributedCache)
    {
        this.distributedCache = distributedCache;
    }

    public async Task<TValue?> GetAsync<TValue>(string key,
        CancellationToken cancellationToken = default)
    {
        string? cacheValue = await distributedCache.GetStringAsync(key, cancellationToken);
        if (cacheValue is null)
        {
            return default;
        }

        return JsonConvert.DeserializeObject<TValue>(cacheValue);
    }

    public bool IsConnected() => distributedCache != null;

    public async Task RemoveAsync(string key)
    {
        await distributedCache.RemoveAsync(key);
    }

    public async Task SetAsync<TValue>(string key,
        TValue value,
        int slidingExpiration = 900,
        int absoluteExpiration = 900,
        CancellationToken cancellationToken = default)
    {
        await distributedCache.SetStringAsync(key,
            JsonConvert.SerializeObject(value, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
            new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(absoluteExpiration),
                SlidingExpiration = TimeSpan.FromSeconds(slidingExpiration)
            }, cancellationToken);
    }
}