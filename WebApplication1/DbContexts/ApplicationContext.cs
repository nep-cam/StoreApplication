using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;

namespace WebApplication1.DbContexts
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<StoreSupplier> StoreSuppliers { get; set; }
        public ApplicationContext(DbContextOptions options): base(options) { 
        
        }
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreSupplier>()
                .HasOne<Store>()
                .WithMany()
                .HasForeignKey(e => e.StoreId);
            modelBuilder.Entity<StoreSupplier>()
                .HasOne<Supplier>()
                .WithMany()
                .HasForeignKey(e => e.SupplierId);
        }

    }
}
