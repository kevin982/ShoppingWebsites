using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityMicroservice.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityMicroservice.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<ApplicationUser> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "costumer", NormalizedName = "COSTUMER"});
            builder.Entity<IdentityRole>().HasData(new IdentityRole() { Name = "owner", NormalizedName = "OWNER"});
            builder.Entity<IdentityRole>().HasData(new IdentityRole () { Name = "admin", NormalizedName = "ADMIN" });
        }
    }
}
