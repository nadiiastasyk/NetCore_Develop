using System.Collections.Generic;

namespace CocktailBar.Models
{
    public class Order
    {
        public int Id { get; set; }

        public virtual List<Cocktail> Cocktails { get; set; }

        public string CustomerName { get; set; }

        public Status Status { get; set; }
    }

    public enum Status
    {
        InProgress,
        Completed
    }
}
