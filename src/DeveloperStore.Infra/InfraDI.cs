using DeveloperStore.Domain.Repositories;
using DeveloperStore.Infra.Context;
using DeveloperStore.Infra.Context.Identity;
using DeveloperStore.Infra.Repositories;
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
                .AddIdentity<ApplicationUser, IdentityRole>(opt =>
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
            services.AddScoped<IAppDbContext, AppDbContext>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
