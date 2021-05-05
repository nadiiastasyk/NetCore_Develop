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
        private readonly ICacheHelper _cacheHelper;
        private readonly IProcessingChannel _processingChannel;
        private readonly IExternalImageServiceClient _client;

        public ImageController(IProcessingChannel processingChannel, ICacheHelper cacheHelper, IExternalImageServiceClient client)
        {
            _processingChannel = processingChannel;
            _cacheHelper = cacheHelper;
            _client = client;
        }

        public IActionResult Get([FromQuery] string imageName)
        {
            byte[] image = _cacheHelper.ProcessCache(imageName);

            if (image is null)
            {
                image = _client.GetImage();
            }

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
