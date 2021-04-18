using Fiction_DZ6.Constants;
using Fiction_DZ6.Services;
using Fiction_DZ6.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Fiction_DZ6.Controllers
{
    public class ImageController : Controller
    {
        private readonly IExternalImageServiceClient _client;
        private readonly IMemoryCache _cache;
        private readonly IProcessingChannel _processingChannel;

        public ImageController(IExternalImageServiceClient client, IMemoryCache cache, IProcessingChannel processingChannel)
        {
            _client = client;
            _cache = cache;
            _processingChannel = processingChannel;
        }

        public IActionResult Get([FromQuery] string imageName)
        {
            var cache = new CacheHelper(_client, _cache);
            var image = cache.ProcessCache(imageName);

            return new FileContentResult(image, ContentTypes.Jpeg);
        }

        [HttpGet]
        public IActionResult Upload()
        {
            return View(new ImageUploadViewModel { UploadStage = UploadStage.Upload });
        }

        [HttpPost]
        public async Task<IActionResult> Upload(ImageUploadViewModel viewModel)
        {
            if (viewModel.Image.Length > 0)
            {
                //_client.UploadImage(viewModel.Image);
                await _processingChannel.Set(viewModel.Image);
                viewModel.UploadStage = UploadStage.Completed;
                viewModel.Image = null;
            }

            return View(viewModel);
        }
    }
}
