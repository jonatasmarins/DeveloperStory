using DeveloperStore.Domain.Entities;
using DeveloperStore.Infra.Context.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Infra.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>, IAppDbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Store");

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Rating>().ToTable("Ratings");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Rating> Ratings { get; set; }
    }

    public interface IAppDbContext
    {
        DbSet<Product> Products { get; set; }

        DbSet<Rating> Ratings { get; set; }
    }
}
