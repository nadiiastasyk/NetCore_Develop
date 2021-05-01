using Microsoft.AspNetCore.Http;

namespace Fiction_DZ6.Services
{
    public interface IExternalImageServiceClient
    {
        byte[] GetImage();

        void UploadImage(IFormFile image);
    }
}
