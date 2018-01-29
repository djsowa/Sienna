using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using SIENN.DbAccess.Entity;
using SIENN.DbAccess.Repositories;

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
                        .HasKey(x => x.Id);

            modelBuilder.Entity<Product>()
                        .HasIndex(p => new { p.Code }).IsUnique();
            modelBuilder.Entity<ProductCategory>()
                        .HasIndex(p => new { p.Code }).IsUnique();
            modelBuilder.Entity<ProductUnit>()
                        .HasIndex(p => new { p.Code }).IsUnique();
            modelBuilder.Entity<ProductType>()
                        .HasIndex(p => new { p.Code }).IsUnique();

            modelBuilder.Entity<ProductToCategory>()
                        .Property<int>("Id").ValueGeneratedOnAdd();
        }

        public void SeedDb()
        {

            Console.WriteLine("EnsureDeleted");
            Database.EnsureDeleted();
            SaveChanges();
            Console.WriteLine("EnsureCreated");
            Database.EnsureCreated();
            SaveChanges();

            // if (Database.IsNpgsql())
            //     Database.Migrate();

            var type1 = new ProductType() { Code = "TypeCode1", Description = "Description1" };
            var type2 = new ProductType() { Code = "TypeCode2", Description = "Description2" };

            var unit1 = new ProductUnit() { Code = "item", Description = "One item/piece" };
            var unit2 = new ProductUnit() { Code = "kg.", Description = "One kilogram." };

            var category1 = new ProductCategory() { Code = "SKATES", Description = "In-line skates" };
            var category2 = new ProductCategory() { Code = "WAX", Description = "Wax for street." };
            var category3 = new ProductCategory() { Code = "ACC", Description = "Accessories" };
            var category4 = new ProductCategory() { Code = "STREET", Description = "Street equipment" };
            var category5 = new ProductCategory() { Code = "WHEEL", Description = "Wheels" };

            Entry(type1).State = EntityState.Added;
            Entry(type2).State = EntityState.Added;

            Entry(unit1).State = EntityState.Added;
            Entry(unit2).State = EntityState.Added;

            Entry(category1).State = EntityState.Added;
            Entry(category2).State = EntityState.Added;
            Entry(category3).State = EntityState.Added;
            Entry(category4).State = EntityState.Added;
            Entry(category5).State = EntityState.Added;

            SaveChanges();

            var valo1 = new Product()
            {
                Code = "VALOAB.1BLACK",
                Description = "Valo AB.1 2018 Black.",
                Price = 999,
                NextDelivery = DateTime.UtcNow.AddMonths(1).AddTicks(1),
                IsAvailable = false,
                ProductType = type1,
                Unit = unit1
            };
            valo1.Categories.Add(new ProductToCategory(0, category1.Id));
            valo1.Categories.Add(new ProductToCategory(0, category4.Id));

            var valo2 = new Product()
            {
                Code = "VALOAB.1CARBON",
                Description = "Valo AB.1 2018 Carbon Black Edition.",
                Price = 1499,
                NextDelivery = DateTime.UtcNow.AddMonths(1).AddTicks(1),
                IsAvailable = false,
                ProductType = type1,
                Unit = unit1
            };
            valo2.Categories.Add(new ProductToCategory(0, category1.Id));
            valo2.Categories.Add(new ProductToCategory(0, category4.Id));

            var wax1 = new Product()
            {
                Code = "WAX01",
                Description = "Super wax 1",
                Price = 10,
                NextDelivery = DateTime.UtcNow.AddMonths(1).AddTicks(1),
                IsAvailable = true,
                ProductType = type1,
                Unit = unit2
            };
            wax1.Categories.Add(new ProductToCategory(0, category2.Id));
            wax1.Categories.Add(new ProductToCategory(0, category4.Id));


            var wax2 = new Product()
            {
                Code = "WAX02",
                Description = "Super wax 2",
                Price = 50,
                NextDelivery = DateTime.UtcNow.AddMonths(1).AddTicks(1),
                IsAvailable = true,
                ProductType = type1,
                Unit = unit2
            };
            wax2.Categories.Add(new ProductToCategory(0, category2.Id));
            wax2.Categories.Add(new ProductToCategory(0, category3.Id));
            wax2.Categories.Add(new ProductToCategory(0, category4.Id));

            var wheel1 = new Product()
            {
                Code = "UNDERCOVER",
                Description = "Undervcover wheels 80 mm.",
                Price = 50,
                NextDelivery = DateTime.UtcNow.AddMonths(1).AddTicks(1),
                IsAvailable = true,
                ProductType = type1,
                Unit = unit1
            };

            var wheel2 = new Product()
            {
                Code = "UNDERCOVER_RICHI",
                Description = "Undervcover Richi pro model wheels 80 mm.",
                Price = 90,
                NextDelivery = DateTime.UtcNow.AddMonths(1).AddTicks(1),
                IsAvailable = true,
                ProductType = type1,
                Unit = unit1
            };

            var wheel3 = new Product()
            {
                Code = "UNDERCOVER Aragon",
                Description = "Undervcover Aragon wheels 80 mm.",
                Price = 40,
                NextDelivery = DateTime.UtcNow.AddMonths(1).AddTicks(1),
                IsAvailable = false,
                ProductType = type1,
                Unit = unit1
            };

            var wheel4 = new Product()
            {
                Code = "UNDERCOVER BASIC",
                Description = "Undervcover basic wheels 80 mm.",
                Price = 10,
                NextDelivery = DateTime.UtcNow.AddMonths(1).AddTicks(1),
                IsAvailable = true,
                ProductType = type1,
                Unit = unit1
            };

            wheel1.Categories.Add(new ProductToCategory(0, category3.Id));
            wheel1.Categories.Add(new ProductToCategory(0, category4.Id));
            wheel1.Categories.Add(new ProductToCategory(0, category5.Id));

            wheel2.Categories.Add(new ProductToCategory(0, category3.Id));
            wheel2.Categories.Add(new ProductToCategory(0, category4.Id));
            wheel2.Categories.Add(new ProductToCategory(0, category5.Id));

            wheel3.Categories.Add(new ProductToCategory(0, category3.Id));
            wheel3.Categories.Add(new ProductToCategory(0, category4.Id));
            wheel3.Categories.Add(new ProductToCategory(0, category5.Id));

            wheel4.Categories.Add(new ProductToCategory(0, category3.Id));
            wheel4.Categories.Add(new ProductToCategory(0, category4.Id));
            wheel4.Categories.Add(new ProductToCategory(0, category5.Id));

            var productRepo = new ProductRepository(this);

            productRepo.AddAsync(valo1).GetAwaiter().GetResult();
            productRepo.AddAsync(valo2).GetAwaiter().GetResult();
            productRepo.AddAsync(wax1).GetAwaiter().GetResult();
            productRepo.AddAsync(wax2).GetAwaiter().GetResult();
            productRepo.AddAsync(wheel1).GetAwaiter().GetResult();
            productRepo.AddAsync(wheel2).GetAwaiter().GetResult();
            productRepo.AddAsync(wheel3).GetAwaiter().GetResult();
            productRepo.AddAsync(wheel4).GetAwaiter().GetResult();

            SaveChanges();
        }
    }
}