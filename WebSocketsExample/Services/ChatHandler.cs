using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketsExample.Models;

namespace WebSocketsExample.Services
{
    public class ChatHandler : IChatHandler
    {
        private ConcurrentDictionary<string, WebSocket> webSocketConnections;

        public ChatHandler()
        {
            webSocketConnections = new ConcurrentDictionary<string, WebSocket>();
        }

        public async Task Handle(WebSocket webSocket, User user)
        {
            while (webSocket.State == WebSocketState.Open)
            {
                string userName = user.FirstName + user.LastName;
                webSocketConnections.TryAdd(userName, webSocket);

                await Send($"User {userName} entered a chat");


                string message = await Receive(webSocket) + $"(Sent by {userName})";

                if (!string.IsNullOrWhiteSpace(message))
                {
                    await Send(message);
                }
            }
        }

        private static async Task<string> Receive(WebSocket webSocket)
        {
            var arraySegment = new ArraySegment<byte>(new byte[1024]);
            var receiveResult = await webSocket.ReceiveAsync(arraySegment, CancellationToken.None);

            if (receiveResult.MessageType == WebSocketMessageType.Text)
            {
                return Encoding.UTF8.GetString(arraySegment).Trim('\0');
            }

            return null;
        }

        private async Task Send(string message)
        {
            foreach (var connection in webSocketConnections.Values)
            {
                var messageBuffer = Encoding.UTF8.GetBytes(message);
                await connection.SendAsync(messageBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
