using Microsoft.EntityFrameworkCore;
using WebStore.Models;

namespace WebStore.Data
{
    public class WebStoreContext : DbContext
    {
        public WebStoreContext(DbContextOptions<WebStoreContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<StockItem> StockItmes { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<StockItem>().ToTable("StockItem");
            modelBuilder.Entity<Rating>().ToTable("Rating");
            modelBuilder.Entity<Comment>().ToTable("Comment");
        }
    }
}