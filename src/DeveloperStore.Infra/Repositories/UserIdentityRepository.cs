using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Infra.Context;
using DeveloperStore.Infra.Context.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Infra.Repositories
{
    public abstract class UserIdentityRepository<T> : Repository<T> where T : ApplicationUser
    {
        public UserManager<T> _context { get; private set; } = null!;
        private readonly IQueryable<T> _dbSet;

        public UserIdentityRepository(UserManager<T> UserManager, AppDbContext context) : base(context)
        {
            _context = UserManager;
            _dbSet = _context.Users;
        }

        public override async Task<T?> GetByIdAsync(int id, QueryOptions options)
        {
            SetDbOptions(options);
            
            return await _dbSet.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public override async Task<T> AddAsync(T entity)
        {
            await _context.CreateAsync(entity);

            return entity;
        }

        public override async Task AddRangeAsync(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                await _context.CreateAsync(item);
            }
        }

        public override async Task<T> UpdateAsync(T entity)
        {
            await _context.UpdateAsync(entity);

            return entity;
        }

        public override async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.Where(x => x.Id == id).FirstAsync();
            if (entity != null)
            {
                await _context.DeleteAsync(entity);
            }
        }

        public override async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            foreach (var item in entities)
            {
                var entity = await _dbSet.Where(x => x.Id == item.Id).FirstAsync();

                if (entity != null)
                {
                    await _context.DeleteAsync(entity);
                }
            }
        }
    }
}
