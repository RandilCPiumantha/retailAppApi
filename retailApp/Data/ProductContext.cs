using Microsoft.EntityFrameworkCore;
using retailApp.Model;

namespace retailApp.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<DisqualificationReason> DisqualificationReasons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define the one-to-one relationship between Product and DisqualificationReason
            modelBuilder.Entity<Product>()
                .HasOne<DisqualificationReason>()
                .WithOne(d => d.Product)
                .HasForeignKey<DisqualificationReason>(d => d.ProductId);
        }
    }
}
