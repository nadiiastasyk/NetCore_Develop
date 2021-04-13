using Fiction_DZ6.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;

namespace Fiction_DZ6.Controllers
{
    public class ImageController : Controller
    {
        private readonly IExternalImageServiceClient _client;
        private readonly IMemoryCache _cache;

        public ImageController(IExternalImageServiceClient client, IMemoryCache cache)
        {
            _client = client;
            _cache = cache;
        }

        public IActionResult Get()
        {
            var cacheKey = $"image_{DateTime.UtcNow.Date}";
            var image = _cache.Get<byte[]>(cacheKey);

            if (image is null)
            {
                image = _client.GetImage();
                _cache.Set<byte[]>(cacheKey, image);
            }

            return new FileContentResult(image, "image/jpeg");
        }
    }
}
