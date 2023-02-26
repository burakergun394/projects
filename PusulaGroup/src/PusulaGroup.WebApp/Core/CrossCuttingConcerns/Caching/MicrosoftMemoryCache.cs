using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace PusulaGroup.WebApp.Core.CrossCuttingConcerns.Caching
{
    public class MicrosoftMemoryCache : ICache
    {
        private readonly IMemoryCache _memoryCache;
        private List<string> cacheKeys = new List<string>();
        public MicrosoftMemoryCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));
            cacheKeys.Add(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
            cacheKeys.Remove(key);
        }

        public void RemoveKeysByStartingValue(string value)
        {
            var keys = cacheKeys.Where(x => x.StartsWith(value)).ToList();
            var newCacheKeys = cacheKeys.Where(x => !x.StartsWith(value)).ToList();
            foreach(var key in keys)
            {
                if (string.IsNullOrWhiteSpace(key))
                    continue;

                _memoryCache.Remove(key);
            }
            cacheKeys = newCacheKeys;
        }

        public bool IsAdd(string key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

        public bool TryGet<T>(string key, out T value)
        {
            if (!_memoryCache.TryGetValue(key, out object gettingValue))
            {
                value = default;
                return false;
            }

            value = (T)gettingValue;
            return true;
        }
    }
}
