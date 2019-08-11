using Microsoft.Extensions.Caching.Memory;
using System;

namespace BizCover.Api.Cars.Cache
{
    public class CarsMemoryCache<T>
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public T GetOrAdd(object key, Func<T> addItem)
        {
            T entry;

            if(!_cache.TryGetValue(key, out entry))
            {
                entry = addItem();
                _cache.Set(key, entry, new DateTimeOffset(DateTime.Now.AddDays(1)));
            }

            return entry;
        }

        public void Refresh(object key, Func<T> getItem)
        {
            _cache.Remove(key);
            _cache.Set(key, getItem());
        }
    }
}
