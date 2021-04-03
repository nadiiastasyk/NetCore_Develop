using System.Collections.Generic;
using System.Linq;

namespace RestApiExample.Models
{
    public class MockNewsRepository : INewsRepository
    {
        private List<News> _news;

        public MockNewsRepository()
        {
            _news = new List<News>
            {
                new News { Id = 1, Title = "Humanity finally colonized the Mercury!!", Text = "", AuthorName = "Jeremy Clarkson", IsFake = true},
                new News { Id = 2, Title = "Increase your lifespan by 10 years, every morning you need...", Text = "", AuthorName = "Svetlana Sokolova", IsFake = true},
                new News { Id = 3, Title = "Scientists estimed the time of the vaccine invension: it is a summer of 2021", Text = "", AuthorName = "John Jones", IsFake = false },
                new News { Id = 4, Title = "Ukraine reduces the cost of its obligations!", Text = "", AuthorName = "Cerol Denvers", IsFake = false },
                new News { Id = 5, Title = "A species were discovered in Africa: it is blue legless cat", Text = "", AuthorName = "Jimmy Felon", IsFake = true }
            };
        }

        // queries
        public IEnumerable<News> GetNews(bool? isFake)
        {
            if (isFake != null)
            {
                return _news.Where(news => news.IsFake == isFake);
            }
            else
                return _news;
        }

        // DZ 5, Task 5.1
        public void AddNews(News news)
        {
            if (_news.Contains(_news.FirstOrDefault(item => item.Id == news.Id)).Equals(true))
            {
                return;
            }

            else _news.Add(news);
        }

        public void UpdateNews(int id, News newsToUpdate)
        {
            var saved = _news.Where(news => news.Id == id)
                .Select(getNews =>
                {
                    getNews.Id = newsToUpdate.Id;
                    getNews.Text = newsToUpdate.Text;
                    getNews.Title = newsToUpdate.Title;
                    getNews.IsFake = newsToUpdate.IsFake;
                    getNews.AuthorName = newsToUpdate.AuthorName;
                    return getNews;
                });
        }

        // DZ 5, Task 5.2
        public void DeleteNews(int id)
        {
            var itemToDelete = _news.Single(item => item.Id == id);
            _news.Remove(itemToDelete);
        }

        public void UpdateAllNews(List<News> newsToUpdate)
        {
            _news = newsToUpdate;
        }
    }
}
