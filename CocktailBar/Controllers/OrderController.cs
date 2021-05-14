using CocktailBar.Models;
using CocktailBar.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CocktailBar.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly INotificationHandler _notificationHandler;

        public OrderController(IOrdersRepository ordersRepository, INotificationHandler notificationHandler)
        {
            _ordersRepository = ordersRepository;
            _notificationHandler = notificationHandler;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult MyOrders()
        {
            ViewBag.Orders = _ordersRepository.GetOrdersWithDependencies().Where(order => order.CustomerName == "Nadiia");
            return View();
        }

        public async Task Handshake()
        {
            var webSockets = HttpContext.WebSockets;

            if (webSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await webSockets.AcceptWebSocketAsync();

                foreach ( var order in _ordersRepository.GetOrdersWithDependencies().Where(order => order.Status == Status.InProgress))
                {
                    await Task.Delay(TimeSpan.FromSeconds(20));
                    await _notificationHandler.Handle(webSocket, order);
                }

            }
            else
            {
                HttpContext.Response.StatusCode = 400;
            }
        }
    }
}