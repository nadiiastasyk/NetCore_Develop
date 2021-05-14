using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CocktailBar.Models
{
    public class CocktailBarDbContext : IdentityDbContext
    {
        private readonly IConfiguration _configuration;

        public DbSet<Order> Orders { get; set; }

        public DbSet<Cocktail> Cocktail { get; set; }

        public CocktailBarDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("CocktailBarDBConnection"),
                options => options.EnableRetryOnFailure()).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Cocktail>().HasData(
               new Cocktail
               {
                   Id = 1,
                   Name = "Smashed Watermelon Margarita",
                   OrderId = 1
               });

            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    CustomerName = "Nadiia",
                    Status = Status.InProgress
                });
        }
    }
}
