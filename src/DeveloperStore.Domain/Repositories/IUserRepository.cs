using DeveloperStore.Domain.Enums;
using DeveloperStore.Infra.Context.Identity;
using Microsoft.AspNetCore.Identity;

namespace DeveloperStore.Domain.Repositories
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<IdentityResult> AddAsync(ApplicationUser entity, string password);
        Task<IdentityResult> AddToRoleAsync(ApplicationUser entity, Role role);
    }
}
