using System.Collections.Generic;

namespace RestApiExample.Models
{
    public interface INewsRepository
    {
        IEnumerable<News> GetNews(bool? isFake = null);

        void AddNews(News news);

        void UpdateNews(int id, News news);

        void UpdateAllNews(List<News> news);

        void DeleteNews(int id);
    }
}
