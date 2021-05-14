using CocktailBar.Models;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CocktailBar.Services
{
    public class NotificationHandler : INotificationHandler
    {
        private ConcurrentDictionary<string, WebSocket> webSocketConnections;

        public NotificationHandler()
        {
            webSocketConnections = new ConcurrentDictionary<string, WebSocket>();
        }

        public async Task Handle(WebSocket webSocket, Order order)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                string cocktails = string.Join(",", order.Cocktails.Select(cocktail => cocktail.Name));
                string userName = order.CustomerName;
                webSocketConnections.TryAdd(cocktails, webSocket);
                webSocketConnections.TryAdd(userName, webSocket);

                string message = $"{userName}, your delicious cocktails {cocktails} is ready for collection!";
                await Send(message);
            }
        }

        private async Task Send(string message)
        {
            foreach (var connection in webSocketConnections.Values)
            {
                var messageBytes = Encoding.UTF8.GetBytes(message);
                await connection.SendAsync(messageBytes, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
