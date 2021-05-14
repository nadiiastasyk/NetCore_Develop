using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CocktailBar.Models
{
    public class SQLOrdersRepository : IOrdersRepository
    {
        private readonly CocktailBarDbContext _dbContext;

        public SQLOrdersRepository(CocktailBarDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add(Order order)
        {
            _dbContext.Add(order);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetOrders()
        {
           return _dbContext.Orders;
        }

        public IEnumerable<Order> GetOrdersWithDependencies()
        {
            return _dbContext.Orders.Include(cocktail => cocktail.Cocktails);
        }
    }
}
