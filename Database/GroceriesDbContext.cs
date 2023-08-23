using groceries_api.Models;
using Microsoft.EntityFrameworkCore;

namespace groceries_api.Database
{
    public class GroceriesDbContext : DbContext 
    {
        public GroceriesDbContext(DbContextOptions options) : base(options) {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLowerCaseNamingConvention();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Grocery>(entity =>
            {
                entity.HasKey(e => e.Id);
            });
        }
        public DbSet<Grocery> Groceries { get; set; }

    }
}
