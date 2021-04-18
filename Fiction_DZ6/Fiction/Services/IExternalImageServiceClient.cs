using Microsoft.AspNetCore.Http;

namespace Fiction_DZ6.Services
{
    public interface IExternalImageServiceClient
    {
        byte[] GetImage(string imageName = null);

        void UploadImage(IFormFile image);
    }
}
