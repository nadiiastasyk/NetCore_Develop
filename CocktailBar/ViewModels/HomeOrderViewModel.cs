using CocktailBar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailBar.ViewModels
{
    public class HomeOrderViewModel
    {
        public string CocktailName { get; set; }

        public Cocktail[] Cocktails { get; set; }
    }
}
