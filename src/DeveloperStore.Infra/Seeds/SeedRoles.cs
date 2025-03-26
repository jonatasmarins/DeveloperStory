using DeveloperStore.Domain.Enums;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Infra.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DeveloperStore.Infra.Seeds
{
    public class SeedRoles
    {
        public static void InitializeDateBase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            dbContext.Database.EnsureCreated();
        }

        public static void AddRoles(IServiceProvider serviceProvider)
        {
            var logger = new LoggerFactory().CreateLogger<SeedRoles>();

            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            var roles = new string[] {
                Role.Customer.ToString(),
                Role.Manager.ToString(),
                Role.Admin.ToString(),
            };

            foreach (var roleName in roles)
            {
                var roleExist = roleManager.RoleExistsAsync(roleName).Result;
                if (!roleExist)
                {
                    var result = roleManager.CreateAsync(new ApplicationRole() { Name = roleName }).Result;
                    if (result.Succeeded)
                    {
                        logger.LogInformation($"Role {roleName} Created with sucess !");
                    }
                    else
                    {
                        logger.LogError($"Error when try create Role: {roleName}");
                    }
                }
            }
        }
    }
}
