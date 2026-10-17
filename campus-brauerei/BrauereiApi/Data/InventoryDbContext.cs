
    using Microsoft.EntityFrameworkCore;
    using BrauereiApi.Models;

    namespace BrauereiApi.Data
    {
        public class InventoryDbContext : DbContext
        {
            public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
            {
            }

            public DbSet<BeerArticle> BeerArticles { get; set; }
            = null!;
        }
    }
