using Microsoft.AspNetCore.Http;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Fiction_DZ6.Services
{
    public class ImageProcessingChannel : IProcessingChannel
    {
        private Channel<IFormFile> _channel;

        public ImageProcessingChannel()
        {
            _channel = Channel.CreateUnbounded<IFormFile>();
        }

        public async Task Set(IFormFile image)
        {
            await _channel.Writer.WriteAsync(image);
        }

        public async Task<IFormFile> Get()
        {
           return await _channel.Reader.ReadAsync();
        }

        public bool TryRead(out IFormFile image)
        {
            return _channel.Reader.TryRead(out image);
        }
    }
}
