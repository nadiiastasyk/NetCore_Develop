using Microsoft.AspNetCore.Mvc;
using MVC_Web_App.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Web_App.Controllers
{
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<News> allNews = NewsBase.GetNews();
            return View(allNews);
        }

        public IActionResult Get(int index)
        {
            var news = NewsBase.GetNews();
            ViewData["News"] = news.SingleOrDefault(x => x.Id == index);

            return View();
        }
    }
}
