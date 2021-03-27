using System.Collections.Generic;

namespace MVC_Web_App.Models
{
    public interface INewsRepository
    {
        IEnumerable<News> GetNews();
    }
}
