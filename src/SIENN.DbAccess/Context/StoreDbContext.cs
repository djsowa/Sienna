using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using SIENN.DbAccess.Entity;

namespace SIENN.DbAccess.Context
{
    public class StoreDbContext : DbContext
    {
        public StoreDbContext(DbContextOptions<StoreDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> Categories { get; set; }
        public DbSet<ProductUnit> Unints { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductToCategory> ProductsToCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductToCategory>()
                        .HasKey(t => new { t.ProductId, t.CategoryId });

            modelBuilder.Entity<Product>()
                        .HasIndex(p => new {p.Code}).IsUnique();
            modelBuilder.Entity<ProductCategory>()
                        .HasIndex(p => new {p.Code}).IsUnique();
            modelBuilder.Entity<ProductUnit>()
                        .HasIndex(p => new {p.Code}).IsUnique();
            modelBuilder.Entity<ProductType>()
                        .HasIndex(p => new {p.Code}).IsUnique();

            modelBuilder.Entity<ProductToCategory>()
                        .Property<int>("Id").ValueGeneratedOnAdd();
        }
    }
}