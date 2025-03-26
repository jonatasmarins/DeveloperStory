using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Infra.Context;
using DeveloperStore.Infra.Context.Identity;
using DeveloperStore.Infra.Repositories;
using DeveloperStore.Infra.Seeds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperStore.Infra
{
    public static class InfraDI
    {
        public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAppDbContext(configuration);

            services.AddAppIdentityDbContext(configuration);

            services.AddRepositories();

            var serviceProvider = services.BuildServiceProvider();

            SeedRoles.InitializeDateBase(serviceProvider);

            SeedRoles.AddRoles(serviceProvider);

            return services;
        }

        private static IServiceCollection AddAppDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);                       
            });

            return services;
        }

        private static IServiceCollection AddAppIdentityDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services
                .AddIdentity<ApplicationUser, ApplicationRole>(opt =>
                {
                    opt.User.RequireUniqueEmail = true;
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequiredLength = 6;
                    opt.Password.RequireNonAlphanumeric = false;

                    opt.Tokens.PasswordResetTokenProvider = TokenOptions.DefaultProvider;
                    opt.Tokens.EmailConfirmationTokenProvider = TokenOptions.DefaultProvider;
                })
                .AddSignInManager()
                .AddEntityFrameworkStores<AppDbContext>();            

            return services;
        }        

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<RoleManager<ApplicationRole>>();

            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductRepository, ProductRepository>();

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
