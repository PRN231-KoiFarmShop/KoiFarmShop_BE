

namespace ks.application.Services.Interfaces;
public interface ICacheService
{
    // Todo  
    bool IsConnected();
    Task<TValue?> GetAsync<TValue>(string key, CancellationToken cancellationToken = default);
    Task SetAsync<TValue>(string key,
        TValue value,
        int slidingExpiration = 900,
        int absoluteExpiration = 900,
        CancellationToken cancellationToken = default);
    Task RemoveAsync(string key);

}