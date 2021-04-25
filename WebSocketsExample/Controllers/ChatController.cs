using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using WebSocketsExample.Models;
using WebSocketsExample.Services;
using WebSocketsExample.ViewModels;

namespace WebSocketsExample.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatHandler _chatHandler;
        private User _user;

        public ChatController(IChatHandler chatHandler)
        {
            _chatHandler = chatHandler;
            _user = new User();
        }

        // Cannot get user from UserJoinChatViewModel!!!
        public IActionResult Index(UserJoinChatViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                _user.FirstName = viewModel.FirstName;
                _user.LastName = viewModel.LastName;
            }
            return View();
        }

        public async Task Handshake()
        {
            var webSockets = HttpContext.WebSockets;

            if (webSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await webSockets.AcceptWebSocketAsync();

                if (_user.FirstName is not null && _user.LastName is not null)
                {
                    await _chatHandler.Handle(webSocket, _user);
                }
                else Console.WriteLine("User is not specified.");
            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }
    }
}
