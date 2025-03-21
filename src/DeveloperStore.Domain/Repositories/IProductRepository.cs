using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories.Models;

namespace DeveloperStore.Domain.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<string>> GetAllCategories();

        Task<PaginatedResult<IEnumerable<Product>>> GetByCategory(string category, QueryOptions options);
    }
}
