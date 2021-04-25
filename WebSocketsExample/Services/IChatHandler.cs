using System.Net.WebSockets;
using System.Threading.Tasks;
using WebSocketsExample.Models;

namespace WebSocketsExample.Services
{
    public interface IChatHandler
    {
        Task Handle(WebSocket webSocket, User user);
    }
}
