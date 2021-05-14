using System.Collections.Generic;

namespace CocktailBar.Models
{
    public interface ICocktailsRepository
    {
        IEnumerable<Cocktail> GetCocktails();
    }
}
