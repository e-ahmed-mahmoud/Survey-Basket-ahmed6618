using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace SurveyBasket.Services.DistributedCache;

public class CacheService(IDistributedCache disributedCache) : ICacheService
{
    private readonly IDistributedCache _disributedCache = disributedCache;

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = await _disributedCache.GetStringAsync(key, cancellationToken);
        return String.IsNullOrEmpty(cachedValue) ? null : JsonSerializer.Deserialize<T>(cachedValue);

    }
    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        await _disributedCache.SetStringAsync(key, JsonSerializer.Serialize(value), cancellationToken);
    }
    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        await _disributedCache.RemoveAsync(key, cancellationToken);
    }
}