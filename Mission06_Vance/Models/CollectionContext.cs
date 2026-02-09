using Microsoft.EntityFrameworkCore;

namespace Mission06_Vance.Models
{
    public class CollectionContext : DbContext
    {
        public CollectionContext(DbContextOptions<CollectionContext> options) : base(options)
        {
        }

        public DbSet<Collection> Collections { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryID = 1, CategoryName = "Family" },
                new Category { CategoryID = 2, CategoryName = "Horror/Suspense" },
                new Category { CategoryID = 3, CategoryName = "Comedy" },
                new Category { CategoryID = 4, CategoryName = "Drama" },
                new Category { CategoryID = 5, CategoryName = "Action/Adventure" },
                new Category { CategoryID = 6, CategoryName = "Television" },
                new Category { CategoryID = 7, CategoryName = "Miscellaneous" }
            );
        }
    }
}
