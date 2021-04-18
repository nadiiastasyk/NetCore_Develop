using Fiction_DZ6.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RestSharp;
using System;
using System.IO;

namespace Fiction_DZ6.Services
{
    public class ExternalImageServiceClient : IExternalImageServiceClient
    {
        private readonly FictionConfiguration _configuration;

        public ExternalImageServiceClient(IOptions<FictionConfiguration> options)
        {
            _configuration = options.Value;
        }

        public byte[] GetImage(string imageName)
        {
            var client = new RestClient(_configuration.ExternalImageService.ExternalImageServiceUrl);
            var request = new RestRequest(_configuration.ExternalImageService.ExternalImageServiceResource, Method.GET);
            request.AddQueryParameter(_configuration.ExternalImageService.ExternalImageServiceQueryParameter, imageName);

            byte[] result = client.Execute(request).RawBytes;
            return result;
        }

        public void UploadImage(IFormFile image)
        {
            var client = new RestClient(_configuration.ExternalImageService.ExternalImageServiceUrl);
            var request = new RestRequest(_configuration.ExternalImageService.ExternalImageServiceResource, Method.POST);

            using (var stream = new MemoryStream())
            {
                image.CopyTo(stream);
                request.AddJsonBody(Convert.ToBase64String(stream.ToArray()));
                request.AddQueryParameter(_configuration.ExternalImageService.ExternalImageServiceQueryParameter, image.FileName);
                client.Execute(request);
            }
        }
    }
}
