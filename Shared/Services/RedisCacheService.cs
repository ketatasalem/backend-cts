using System.Text.Json;
using StackExchange.Redis;

namespace Labo_Cts_backend.Shared.Services
{
    public class RedisCacheService
    {
        private readonly IDatabase _cacheDb;

        public RedisCacheService(IConnectionMultiplexer redis)
        {
            _cacheDb = redis.GetDatabase();
        }

        public async Task SetCacheAsync<T>(string key, T value, TimeSpan expiration)
        {
            var serializedValue = JsonSerializer.Serialize(value);
            await _cacheDb.StringSetAsync(key, serializedValue, expiration);
        }

        public async Task<T?> GetCacheAsync<T>(string key)
        {
            var value = await _cacheDb.StringGetAsync(key);
            return value.HasValue ? JsonSerializer.Deserialize<T>(value) : default;
        }

        public async Task RemoveCacheAsync(string key)
        {
            await _cacheDb.KeyDeleteAsync(key);
        }
    }
}
