using DeveloperStore.Domain.Enums;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Infra.Context;
using DeveloperStore.Infra.Context.Identity;
using Microsoft.AspNetCore.Identity;

namespace DeveloperStore.Infra.Repositories
{
    public class UserRepository(UserManager<ApplicationUser> userManager, AppDbContext context) : UserIdentityRepository<ApplicationUser>(userManager, context), IUserRepository
    {
        public async Task<IdentityResult> AddAsync(ApplicationUser entity, string password)
        {
            return await userManager.CreateAsync(entity, password);
        }

        public async Task<IdentityResult> AddToRoleAsync(ApplicationUser entity, Role role)
        {
            return await userManager.AddToRoleAsync(entity, role.ToString());
        }
    }
}
