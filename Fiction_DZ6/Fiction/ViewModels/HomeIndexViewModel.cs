using Fiction_DZ6.Models;
using System.Collections.Generic;

namespace Fiction_DZ6.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Character> Characters { get; set; }

        public IEnumerable<Story> Stories { get; set; }
    }
}
