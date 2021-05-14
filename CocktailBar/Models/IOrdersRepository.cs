using System.Collections.Generic;

namespace CocktailBar.Models
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> GetOrders();

        IEnumerable<Order> GetOrdersWithDependencies();

        void Add(Order order);
    }
}
