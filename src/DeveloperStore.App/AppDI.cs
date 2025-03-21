using Microsoft.Extensions.DependencyInjection;

namespace DeveloperStore.App
{
    public static class AppDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppDI).Assembly);

            return services;
        }
    }
}
