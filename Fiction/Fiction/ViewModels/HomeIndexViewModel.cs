using Fiction.Models;
using System.Collections.Generic;

namespace Fiction.ViewModels
{
    public class HomeIndexViewModel
    {
        public IEnumerable<Character> Characters { get; set; }

        public IEnumerable<Story> Stories { get; set; }
    }
}
