using CocktailBar.Models;
using CocktailBar.Services;
using CocktailBar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CocktailBar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICocktailServiceClient _client;
        private readonly IOrdersRepository _ordersRepository;


        public HomeController(ICocktailServiceClient client, IOrdersRepository ordersRepository)
        {
            _client = client;
            _ordersRepository = ordersRepository;
        }

        public IActionResult Index(HomeOrderViewModel viewModel)
        {
            Cocktail[] result = _client.GetCocktails();
            viewModel.Cocktails = result;
            return View(viewModel);
        }

        [HttpGet]
        public IActionResult Order()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Order(HomeOrderViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var cocktail = new Cocktail
                {
                    Id = 3,
                    Name = viewModel.CocktailName,
                    OrderId = _ordersRepository.GetOrders().Last().Id + 1,

                };

                if (!_ordersRepository.GetOrders().Any(o => o.Status == Status.InProgress))
                {
                    var order = new Order
                    {
                        Id = _ordersRepository.GetOrders().Last().Id + 1,
                        CustomerName = User.Identity.Name,
                        Cocktails = new List<Cocktail> { cocktail },
                        Status = Status.InProgress
                    };
                    _ordersRepository.Add(order);
                }
                else
                {
                    _ordersRepository.GetOrders().First(o => o.Status == Status.InProgress)
                        .Cocktails.Add(cocktail);
                }
            }

            return RedirectToAction("MyOrders", "Order");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
