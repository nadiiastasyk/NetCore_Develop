using Fiction_DZ6.Infrastructure;
using Fiction_DZ6.Models;
using Fiction_DZ6.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Fiction_DZ6.Controllers
{
    public class HomeController : Controller
    {
        private IConfiguration _configuration;
        private readonly FictionConfiguration _fictionConfiguration;

        public HomeController(IOptions<FictionConfiguration> fictionConfiguration, IConfiguration configuration)
        {
            _fictionConfiguration = fictionConfiguration.Value;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
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

        [HttpGet]
        public IActionResult SendMessage()
        {
            // not finished
            if (ViewData["MessageType"] == "Email")
            {
                _configuration.Bind("Email");
            }

            return View();
        }

        public void SendMessage([FromServices] IMessageSender messageSender)
        {
            messageSender.SendMessage();
        }
    }
}
