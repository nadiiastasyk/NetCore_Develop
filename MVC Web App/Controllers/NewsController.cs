using Microsoft.AspNetCore.Mvc;
using MVC_Web_App.Models;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Web_App.Controllers
{
    public class NewsController : Controller
    {
        private NewsDbContext _dbContext;

        public NewsController(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            IEnumerable<News> allNews = _dbContext.News.AsParallel();
            return View(allNews);
        }

        [Route("[controller]/[action]/{index:int}")]
        public IActionResult Get(int index)
        {
            ViewData["News"] = _dbContext.News.FirstOrDefault(x => x.Id == index);

            return View();
        }
    }
}
