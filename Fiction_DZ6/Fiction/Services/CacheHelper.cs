using Microsoft.Extensions.Caching.Memory;
using System;

namespace Fiction_DZ6.Services
{
    public class CacheHelper : ICacheHelper
    {
        private readonly IMemoryCache _cache;

        public CacheHelper(IMemoryCache cache)
        {
            _cache = cache;
        }

        public byte[] ProcessCache(string imageName)
        {
            var callInterval = TimeSpan.FromMinutes(2);
            var cacheKey = $"{imageName}_{DateTime.UtcNow.Date}";
            byte[] image = _cache.Get<byte[]>(cacheKey);

            if (image is not null)
            {
                MemoryCacheEntryOptions options = new MemoryCacheEntryOptions();
                options.AbsoluteExpirationRelativeToNow = callInterval;

                _cache.Set<byte[]>(cacheKey, image, options);
            }

            return image;
        }
    }
}
