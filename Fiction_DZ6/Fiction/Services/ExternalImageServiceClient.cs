using RestSharp;
using System;

namespace Fiction_DZ6.Services
{
    public class ExternalImageServiceClient : IExternalImageServiceClient
    {
        public byte[] GetImage()
        {
            var client = new RestClient("http://localhost:63219/");
            var request = new RestRequest("Image", Method.GET);

            var result = client.Execute(request).RawBytes;
            return result;
        }
    }
}
