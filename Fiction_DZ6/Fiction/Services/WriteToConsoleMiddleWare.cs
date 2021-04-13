using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Fiction_DZ6.Services
{
    public class WriteToConsoleMiddleWare
    {
        private RequestDelegate _request;

        private string _output;

        // all the properties should be injected here
        public WriteToConsoleMiddleWare(RequestDelegate request, string output)
        {
            _request = request;
            _output = output;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine(_output);
            await _request(context);
        }
    }
}
