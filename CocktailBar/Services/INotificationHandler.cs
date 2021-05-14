using CocktailBar.Models;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace CocktailBar.Services
{
    public interface INotificationHandler
    {
        Task Handle(WebSocket webSocket, Order order);
    }
}
