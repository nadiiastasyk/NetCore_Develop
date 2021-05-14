using CocktailBar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailBar.Services
{
    public interface ICocktailServiceClient
    {
        Cocktail[] GetCocktails();
    }
}
