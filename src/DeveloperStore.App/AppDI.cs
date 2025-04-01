using DeveloperStore.Domain.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperStore.App
{
    public static class AppDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppDI).Assembly);

            services.AddValidatorsFromAssemblyContaining<ProductValidator>();

            services.AddFluentValidationAutoValidation(opt =>
            {
                opt.DisableDataAnnotationsValidation = true;
            });

            return services;
        }
    }
}
