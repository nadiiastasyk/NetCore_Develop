using Fiction_DZ6.Infrastructure;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Fiction_DZ6.Services
{
    public class LoadImageService : BackgroundService
    {
        private readonly IExternalImageServiceClient _client;
        private readonly ICacheHelper _cacheHelper;
        private readonly IFictionConfiguration _configuration;

        public LoadImageService(IExternalImageServiceClient client, ICacheHelper cacheHelper, IOptions<IFictionConfiguration> configuration)
        {
            _client = client;
            _cacheHelper = cacheHelper;
            _configuration = configuration.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var imageName = _configuration.ImageName;
                var callInterval = TimeSpan.FromMinutes(2);
                byte[] image =_cacheHelper.ProcessCache(imageName);

                if (image is null)
                {
                    image = _client.GetImage();
                }

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
