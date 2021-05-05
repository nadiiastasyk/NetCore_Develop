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
        private readonly IRestClient _restClient;
        private readonly IFictionConfiguration _configuration;

        public ExternalImageServiceClient(IOptions<IFictionConfiguration> configuration, IRestClient restClient)
        {
            _configuration = configuration.Value;
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(_configuration.ExternalImageService.ExternalImageServiceUrl);
        }

        public byte[] GetImage()
        {
            try
            {
                var request = new RestRequest(_configuration.ExternalImageService.ExternalImageServiceResource, Method.GET);
                request.AddQueryParameter(_configuration.ExternalImageService.ExternalImageServiceQueryParameter, _configuration.ImageName);

                byte[] result = _restClient.Execute(request)?.RawBytes;
                return result;
            }
            catch
            {
                return null;
            }
            
        }

        public void UploadImage(IFormFile image)
        {
            var client = new RestClient(_configuration.ExternalImageService.ExternalImageServiceUrl);
            var request = new RestRequest(_configuration.ExternalImageService.ExternalImageServiceResource, Method.POST);

            using var stream = new MemoryStream();
            {
                image.CopyTo(stream);
                request.AddJsonBody(Convert.ToBase64String(stream.ToArray()));
                request.AddQueryParameter(_configuration.ExternalImageService.ExternalImageServiceQueryParameter, image.FileName);
                client.Execute(request);
            }
        }
    }
}
