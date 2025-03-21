using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace DeveloperStore.Infra.Repositories
{
    public abstract class Repository<T> : IRepository<T> where T : Entity
    {
        public AppDbContext _context { get; private set; } = null!;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;     
            _dbSet = _context.Set<T>();
        }

        public async Task<PaginatedResult<IEnumerable<T>>> GetAllAsync(QueryOptions options)
        {
            var query = SetDbOptions(options);

            var totalItems = await query.CountAsync(); 

            IEnumerable<T> result = await query.Skip((options.Page - 1) * options.Size).Take(options.Size).ToListAsync();

            return new PaginatedResult<IEnumerable<T>>()
            {
                Data = result,
                CurrentPage = options.Page,
                PageSize = options.Size,
                TotalItems = totalItems
            };
        }

        public async Task<PaginatedResult<IEnumerable<T>>> GetWhereAsync(Expression<Func<T, bool>> predicate, QueryOptions options)
        {
            var query = SetDbOptions(options);

            var totalItems = await query.Where(predicate).CountAsync();

            IEnumerable<T> result = await query.Skip((options.Page - 1) * options.Size).Take(options.Size).Where(predicate).ToListAsync();

            return new PaginatedResult<IEnumerable<T>>()
            {
                Data = result,
                CurrentPage = options.Page,
                PageSize = options.Size,
                TotalItems = totalItems
            };            
        }

        public async Task<T?> GetByIdAsync(int id, QueryOptions options)
        {
            SetDbOptions(options);
            
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);            

            return entity;
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                await _dbSet.AddAsync(item);
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            await Task.FromResult(_dbSet.Update(entity));

            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                Entity? ent = item as Entity;
                var entity = await _dbSet.FindAsync(ent?.Id);
                if (entity != null)
                {
                    _dbSet.Remove(entity);
                }
            }
        }

        private IQueryable<T> SetDbOptions(QueryOptions options)
        {
            var query = _dbSet.AsQueryable();

            if (options.IsAsNoTracking) query = query.AsNoTracking();

            if (options.IsIgnoreAutoIncludes) query = query.IgnoreAutoIncludes();

            return Repository<T>.SetDbQueryOrder(query, options);
        }

        private static IQueryable<T> SetDbQueryOrder(IQueryable<T> query, QueryOptions options)
        {
            if (!string.IsNullOrEmpty(options.Order))
            {
                var ordenacaoLista = options.Order.Split(',')
                    .Select(o =>
                    {
                        var parts = o.Trim().Split(' ');
                        var property = parts[0];
                        var direction = parts.Length > 1 && parts[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? "desc" : "asc";
                        return new { Property = property, Direction = direction };
                    })
                    .ToList();

                foreach (var criterio in ordenacaoLista)
                {
                    if (criterio.Direction == "asc")
                    {
                        query = query.OrderBy(e => EF.Property<object>(e, criterio.Property));
                    }
                    else
                    {
                        query = query.OrderByDescending(e => EF.Property<object>(e, criterio.Property));
                    }
                }
            }

            return query;
        }
    }
}
