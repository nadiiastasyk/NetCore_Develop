using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Fiction_DZ6.Services
{
    public interface IProcessingChannel
    {
        Task Set(IFormFile image);

        Task<IFormFile> Get();

        bool TryRead(out IFormFile image);
    }
}
