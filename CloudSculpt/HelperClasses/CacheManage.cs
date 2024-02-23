using Microsoft.Extensions.Caching.Memory;

namespace CloudSculpt.HelperClasses;

public static class CacheManage
{
    public static void SaveToCache(string key, string value)
    {
        var cache = new MemoryCache(new MemoryCacheOptions());
        cache.Set(key, value);
    }

    public static string? GetFromCache(string key)
    {
        var result = string.Empty;
        
        var cache = new MemoryCache(new MemoryCacheOptions());
        if (cache.TryGetValue(key, out string? cachedValue))
        {
            result = cachedValue;
        }

        return result;
    }
}