using Fiction_DZ6.Infrastructure;
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
        private readonly IFictionConfiguration _configuration;

        public LoadImageService(IExternalImageServiceClient client, IMemoryCache cache, IFictionConfiguration configuration)
        {
            _client = client;
            _cache = cache;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var imageName = _configuration.ImageName;
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
