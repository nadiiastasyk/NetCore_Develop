using System.Collections.Generic;

namespace MVC_Web_App.Models
{
    // DZ 4. Task 2
    public class SQLNewsRepository : INewsRepository
    {
        private readonly NewsDbContext _dbContext;

        public SQLNewsRepository(NewsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<News> GetNews()
        {
            return _dbContext.News;
        }
    }
}
