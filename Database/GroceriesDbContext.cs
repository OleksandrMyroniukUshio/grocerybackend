using groceries_api.Models.Groceries;
using groceries_api.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace groceries_api.Database
{
    public class GroceriesDbContext : DbContext
    {
        public GroceriesDbContext(DbContextOptions options) : base(options) {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Grocery>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.User)
                      .WithMany(u => u.Groceries)
                      .HasForeignKey(e => e.UserId);
            });
            modelBuilder.Entity<User>()
            .HasIndex(u => u.Username)
            .IsUnique();
        }
        public DbSet<Grocery> Groceries { get; set; }
        public DbSet<User> Users { get; set; }

    }
}
