using Microsoft.AspNetCore.Http;

namespace Fiction_DZ6.ViewModels
{
    public class ImageUploadViewModel
    {
        public IFormFile Image { get; set; }

        public UploadStage UploadStage { get; set; }
    }

    public enum UploadStage
    {
        Upload,
        Completed
    }
}
