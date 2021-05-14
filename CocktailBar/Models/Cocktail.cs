using System.Collections.Generic;

namespace CocktailBar.Models
{
    public class Cocktail
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Ingredients { get; set; }

        public string Image { get; set; }

        public int? OrderId { get; set; }

        public virtual Order Order { get; set; }
    }
}
