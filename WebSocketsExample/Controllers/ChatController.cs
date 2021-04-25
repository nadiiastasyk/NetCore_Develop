using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using WebSocketsExample.Models;
using WebSocketsExample.Services;

namespace WebSocketsExample.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatHandler _chatHandler;

        public ChatController(IChatHandler chatHandler)
        {
            _chatHandler = chatHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        // How to get user from View or Model?
        public async Task Handshake(User user)
        {
            var webSockets = HttpContext.WebSockets;

            if (webSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await webSockets.AcceptWebSocketAsync();
  
                await _chatHandler.Handle(webSocket, user);
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }
    }
}
