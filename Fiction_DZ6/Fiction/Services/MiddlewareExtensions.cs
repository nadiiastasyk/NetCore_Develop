using Microsoft.AspNetCore.Builder;

namespace Fiction_DZ6.Services
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseWriteToConsole(this IApplicationBuilder app, string output)
        {
           return app.UseMiddleware<WriteToConsoleMiddleWare>(output);
        }
    }
}
