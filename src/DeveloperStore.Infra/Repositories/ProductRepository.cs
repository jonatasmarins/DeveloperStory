using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Infra.Repositories
{
    public class ProductRepository(AppDbContext context) : Repository<Product>(context), IProductRepository
    {
        public async Task<IEnumerable<string>?> GetAllCategories()
        {
            return await context.Products.AsNoTracking().Select(x => x.Category).Distinct().OrderBy(x => x).ToListAsync();
        }

        public Task<PaginatedResult<IEnumerable<Product>>> GetByCategory(string category, QueryOptions options)
        {
            return GetWhereAsync(x => EF.Functions.Like(x.Category.Trim().ToLower(), $"%{category.Trim().ToLower()}%"), options);
        }
    }
}
