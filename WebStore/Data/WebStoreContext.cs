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
        public DbSet<StockItem> StockItems { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseItem> PurchaseItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PurchaseItem>()
                .HasKey(pi => new { pi.PurchaseId, pi.StockItemId });

            modelBuilder.Entity<PurchaseItem>()
                .HasOne(p => p.Purchase)
                .WithMany(pi => pi.PurchaseItems)
                .HasForeignKey(pt => pt.PurchaseId);
            

            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<StockItem>().ToTable("StockItem");
            modelBuilder.Entity<Rating>().ToTable("Rating");
            modelBuilder.Entity<Comment>().ToTable("Comment");
            modelBuilder.Entity<Purchase>().ToTable("Purchase");
            modelBuilder.Entity<PurchaseItem>().ToTable("PurchaseItem");
        }
    }
}