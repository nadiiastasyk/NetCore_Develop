using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Fiction_DZ6.Models
{
    public class FictionDbContext : DbContext
    {
        public DbSet<Character> Characters { get; set; }

        public DbSet<Story> Story { get; set; }

        private IConfiguration _configuration { get; set; }
        public FictionDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("FictionDbConnectionNew")).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Character>().HasData(
             new Character { Id = 1, Name = "Finn Mertens", Age = 14, StoryId = 1 },
                new Character { Id = 2, Name = "Philip Fry", Age = 25, StoryId = 2 },
                new Character { Id = 3, Name = "Arven Undomiel", Age = 2700, StoryId = 3 });

            modelBuilder.Entity<Story>().HasData(
                new Story { Id = 1, Name = "Adventure Time" },
                new Story { Id = 2, Name = "Futurama" },
                new Story { Id = 3, Name = "LOTR" });
        }
    }
}
