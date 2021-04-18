using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fiction_DZ6.Services
{
    public class LoadImageService : BackgroundService
    {
        private readonly IExternalImageServiceClient _client;
        private readonly IMemoryCache _cache;

        public LoadImageService(IExternalImageServiceClient client, IMemoryCache cache)
        {
            _client = client;
            _cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // TODO: pass imageName as a constructor parameter. Not sure how to send here imageName
                var imageName = "Image_name.png";
                var callInterval = TimeSpan.FromMinutes(2);
                var cache = new CacheHelper(_client, _cache);
                cache.ProcessCache(imageName);
               
                await Task.Delay(callInterval);

                // Recieve information about the image to get
                // Check if image is in cache
                // Get image from ExternalImageServiceClient
                // Add image to cache (if it was fetched)
                // Return image
            }

        }
    }
}
