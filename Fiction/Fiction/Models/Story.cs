using System.Collections.Generic;

namespace Fiction.Models
{
    public class Story
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Character> Characters { get; set; }
    }
}
