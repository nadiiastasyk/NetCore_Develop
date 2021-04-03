using Microsoft.AspNetCore.Mvc;
using RestApiExample.Models;
using System.Collections.Generic;

namespace RestApiExample.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _newsRepository;

        public NewsController(INewsRepository newsRepository)
        {
            _newsRepository = newsRepository;
        }

        // DZ 5, Task 4
        [HttpGet]
        public IEnumerable<News> Get([FromHeader] bool? isFake)
        {
            if (isFake != null)
            {
                return _newsRepository.GetNews(isFake);
            }
            else
                return _newsRepository.GetNews();
        }

        [HttpPost]
        public void Add([FromBody] News news)
        {
            _newsRepository.AddNews(news);
        }

        // DZ 5, Task 2
        [HttpPatch("{id}")]
        public void Update([FromRoute] int id, [FromBody] News news)
        {
            _newsRepository.UpdateNews(id, news);
        }

        // DZ 5, Task 3
        [HttpPut()]
        public void UpdateAll([FromBody] List<News> news)
        {
            _newsRepository.UpdateAllNews(news);
        }

        [HttpDelete("{id}")]
        public void Delete([FromRoute] int id)
        {
            _newsRepository.DeleteNews(id);
        }

        [HttpDelete]
        public void DeleteFromHeader([FromHeader] int id)
        {
            _newsRepository.DeleteNews(id);
        }
    }
}
