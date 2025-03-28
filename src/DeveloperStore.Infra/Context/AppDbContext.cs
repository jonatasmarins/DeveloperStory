using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Infra.Context.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Infra.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<ApplicationUser, ApplicationRole, int, IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>(options), IAppDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("Store");

            modelBuilder.Entity<Product>().ToTable("Products");
            modelBuilder.Entity<Rating>().ToTable("Ratings");
            modelBuilder.Entity<Cart>().ToTable("Carts");
            modelBuilder.Entity<CartProduct>().ToTable("CartProduct");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);                

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Product> Products { get; set; }

        public DbSet<Rating> Ratings { get; set; }

        public DbSet<Cart> Carts { get; set; }

        public DbSet<CartProduct> CartProducts { get; set; }
    }

    public interface IAppDbContext
    {
        DbSet<Product> Products { get; set; }

        DbSet<Rating> Ratings { get; set; }

        DbSet<Cart> Carts { get; set; }

        DbSet<CartProduct> CartProducts { get; set; }
    }
}
