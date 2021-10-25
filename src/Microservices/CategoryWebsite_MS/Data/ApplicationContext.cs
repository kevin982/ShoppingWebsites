using System;
using CategoryWebsite_MS.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CategoryWebsite_MS.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<WebsiteCategory> WebsiteCategories { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options):base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WebsiteCategory>().HasData(new WebsiteCategory { WebsiteCategoryId = Guid.NewGuid(), WebsiteCategoryName = "Department Store" });
            modelBuilder.Entity<WebsiteCategory>().HasData(new WebsiteCategory { WebsiteCategoryId = Guid.NewGuid(), WebsiteCategoryName = "Speciality Store" });
            modelBuilder.Entity<WebsiteCategory>().HasData(new WebsiteCategory { WebsiteCategoryId = Guid.NewGuid(), WebsiteCategoryName = "Supermarket" });
            modelBuilder.Entity<WebsiteCategory>().HasData(new WebsiteCategory { WebsiteCategoryId = Guid.NewGuid(), WebsiteCategoryName = "Convenience Store" });
            modelBuilder.Entity<WebsiteCategory>().HasData(new WebsiteCategory { WebsiteCategoryId = Guid.NewGuid(), WebsiteCategoryName = "Discount Store" });
            modelBuilder.Entity<WebsiteCategory>().HasData(new WebsiteCategory { WebsiteCategoryId = Guid.NewGuid(), WebsiteCategoryName = "Hypermarket" });
            modelBuilder.Entity<WebsiteCategory>().HasData(new WebsiteCategory { WebsiteCategoryId = Guid.NewGuid(), WebsiteCategoryName = "Warehouse Store" });
            modelBuilder.Entity<WebsiteCategory>().HasData(new WebsiteCategory { WebsiteCategoryId = Guid.NewGuid(), WebsiteCategoryName = "E-Commerce" });
            modelBuilder.Entity<WebsiteCategory>().HasData(new WebsiteCategory { WebsiteCategoryId = Guid.NewGuid(), WebsiteCategoryName = "Drug Store" });
        }
    }
}