using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Infra.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace DeveloperStore.Tests.Repositories.Products
{
    public class ProductRepositoryTestFixture : IProductRepository
    {
        public ServiceProvider Provider { get; set; }

        public RepositoryHelper RepositoryHelper { get; set; }

        public ProductRepositoryTestFixture()
        {     
            var services = new ServiceCollection();

            services.AddInfra();

            Provider = services.BuildServiceProvider();

            RepositoryHelper = new RepositoryHelper(Provider);

            var context = Provider.GetRequiredService<AppDbContext>();

            context.Database.EnsureDeleted();

            context.Database.EnsureCreated();
        }

        public async Task AddProducts(IEnumerable<Product> products)
        {
            await RepositoryHelper.ExecuteAsync<IProductRepository>(async repo => await repo.AddRangeAsync(products));
        }

        public async Task<PaginatedResult<IEnumerable<Product>>> GetAllAsync(QueryOptions options)
        {
            return await RepositoryHelper.ExecuteAsync<IProductRepository, PaginatedResult<IEnumerable<Product>>>(async repo => await repo.GetAllAsync(options));
        }

        public async Task<Product> GetByIdAsync(int id, QueryOptions options)
        {
            return await RepositoryHelper.ExecuteAsync<IProductRepository, Product>(async repo => await repo.GetByIdAsync(id, options));
        }

        public async Task<Product> AddAsync(Product entity)
        {
            return await RepositoryHelper.ExecuteAsync<IProductRepository, Product>(async repo => await repo.AddAsync(entity));
        }

        public async Task AddRangeAsync(IEnumerable<Product> entities)
        {
            await RepositoryHelper.ExecuteAsync<IProductRepository>(async repo => await repo.AddRangeAsync(entities));
        }

        public async Task<Product> UpdateAsync(Product entity)
        {
            return await RepositoryHelper.ExecuteAsync<IProductRepository, Product>(async repo => await repo.UpdateAsync(entity));
        }

        public async Task DeleteAsync(int id)
        {
            await RepositoryHelper.ExecuteAsync<IProductRepository>(async repo => await repo.DeleteAsync(id));
        }

        public async Task DeleteRangeAsync(IEnumerable<Product> entities)
        {
            await RepositoryHelper.ExecuteAsync<IProductRepository>(async repo => await repo.DeleteRangeAsync(entities));
        }

        public Task<IEnumerable<string>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<IEnumerable<Product>>> GetByCategory(string category, QueryOptions options)
        {
            throw new NotImplementedException();
        }

        public Task<PaginatedResult<IEnumerable<Product>>> GetWhereAsync(Expression<Func<Product, bool>> predicate, QueryOptions options)
        {
            throw new NotImplementedException();
        }
    }
}