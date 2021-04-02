using System.Collections.Generic;

namespace MVC_Web_App.Models
{
    public static class NewsInitialData
    {
        private static IEnumerable<News> _news = new List<News>
        {
            new News { Id = 1, Title = "Humanity finally colonized the Mercury!!", Text = "", AuthorName = "Jeremy Clarkson", IsFake = true},
            new News { Id = 2, Title = "Increase your lifespan by 10 years, every morning you need...", Text = "", AuthorName = "Svetlana Sokolova", IsFake = true},
            new News { Id = 3, Title = "Scientists estimed the time of the vaccine invension: it is a summer of 2021", Text = "", AuthorName = "John Jones", IsFake = false },
            new News { Id = 4, Title = "Ukraine reduces the cost of its obligations!", Text = "", AuthorName = "Cerol Denvers", IsFake = false },
            new News { Id = 5, Title = "A species were discovered in Africa: it is blue legless cat", Text = "", AuthorName = "Jimmy Felon", IsFake = true }
        };


        public static IEnumerable<News> News => _news;

    }
}
