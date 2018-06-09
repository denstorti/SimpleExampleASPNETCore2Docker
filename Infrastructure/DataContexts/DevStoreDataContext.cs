using DevStore.Infrastructure.Mappings;
using Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.DataContexts
{
    public class DevStoreDataContext: DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products{ get; set; }

        public static string ConnectionString;

        public DevStoreDataContext()
        {
        }

        public DevStoreDataContext(DbContextOptions<DevStoreDataContext> options): base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString ?? @"Server=tcp:192.168.1.9,1433;Database=DevStore;User Id=sa;Password=Testing123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Category>().HasData(
            //        new { Id = 1, Title = "Category1"},
            //        new { Id = 1, Title = "Category2" }
            //    );

            //modelBuilder.Entity<Product>().HasData(
            //        new { Id = 1, Title = "Product1", CategoryId = 1 },
            //        new { Id = 2, Title = "Product2", CategoryId = 1 },
            //        new { Id = 3, Title = "Product3", CategoryId = 2 }
            //    );

            modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new ProductEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);

        }
    }
    
}
