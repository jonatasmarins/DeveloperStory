using DeveloperStore.Domain.Repositories;
using DeveloperStore.Infra.Context;
using DeveloperStore.Infra.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperStore.Tests
{
    public static class DITest
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            services.AddAppDbContext();

            services.AddRepositories();

            return services;
        }

        private static IServiceCollection AddAppDbContext(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseInMemoryDatabase("Store");
            });

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}
