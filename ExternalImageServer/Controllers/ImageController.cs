using Microsoft.AspNetCore.Mvc;

namespace ExternalImageService.Controllers
{
    [ApiController]
    public class ImageController : Controller
    {
        [HttpGet("Image")]
        public IActionResult GetImage()
        {
            byte[] imageBytes = System.IO.File.ReadAllBytes("wwwroot/pyramid_history.jpg");
            return new FileContentResult(imageBytes, "image/jpeg");
        }
    }
}
