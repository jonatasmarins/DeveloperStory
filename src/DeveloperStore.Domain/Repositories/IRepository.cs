using DeveloperStore.Domain.Repositories.Models;
using System.Linq.Expressions;

namespace DeveloperStore.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<PaginatedResult<IEnumerable<T>>> GetAllAsync(QueryOptions options);
        Task<PaginatedResult<IEnumerable<T>>> GetWhereAsync(Expression<Func<T, bool>> predicate, QueryOptions options);
        Task<T?> GetByIdAsync(int id, QueryOptions options);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteRangeAsync(IEnumerable<T> entities);
    }
}
