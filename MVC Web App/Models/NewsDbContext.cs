using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MVC_Web_App.Models
{
    public class NewsDbContext : DbContext
    {
        // Models we want to map to the db
        public DbSet<News> News { get; set; }


        private IConfiguration _configuration;

        public NewsDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // configure DbContextOptions to connect to the db
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("NewsDbConnection"));
        }

        // create initial data on first usage of DbContext or when creating new migration
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<News>().HasData(NewsInitialData.News);
        }
    }
}
