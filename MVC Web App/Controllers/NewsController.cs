using Microsoft.AspNetCore.Mvc;
using MVC_Web_App.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Web_App.Controllers
{
    public class NewsController : Controller
    {
        private INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<News> allNews = _newsRepository.GetNews();
            return View(allNews);
        }

        public IActionResult Get(int index)
        {
            var news = _newsRepository.GetNews();
            ViewData["News"] = news.SingleOrDefault(x => x.Id == index);

            return View();
        }
    }
}
