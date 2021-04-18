using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;

namespace ExternalImageService.Controllers
{
    [ApiController]
    public class ImageController : Controller
    {
        [HttpGet("Image")]
        public IActionResult GetImage([FromQuery] string imageName)
        {
            string imagePath = Path.Combine("wwwroot", imageName);

            if (System.IO.File.Exists(imagePath))
            {
                byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);
                return new FileContentResult(imageBytes, "image/jpeg");
            }
            else
            {
                throw new FileNotFoundException("The file was not found.", imagePath);
            }
        }

        [HttpPost("Image")]
        public void Upload([FromBody] string image, [FromQuery] string imageName)
        {
            var imagePath = Path.Combine("wwwroot", imageName);
            byte[] imageBytes = Convert.FromBase64String(image);

            System.IO.File.WriteAllBytes(imagePath, imageBytes);
        }
    }
}
