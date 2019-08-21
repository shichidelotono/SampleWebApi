using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace BizCover.Api.Cars.Cache
{
    public class CarsMemoryCache<T>
    {
        private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        public async Task<T> GetOrAdd(object key, Func<Task<T>> addItem)
        {
            T entry;

            if(!_cache.TryGetValue(key, out entry))
            {
                entry = await addItem();
                _cache.Set(key, entry, new DateTimeOffset(DateTime.Now.AddDays(1)));                               
            }

            return entry;
        }

        public async Task Refresh(object key, Func<Task<T>> getItem)
        {
            await Task.Run(() => {
                _cache.Remove(key);
                _cache.Set(key, getItem());
            });
        }
    }
}
