using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Infra.Context;

namespace DeveloperStore.Infra.Repositories
{
    public class CartRepository(AppDbContext context) : Repository<Cart>(context), ICartRepository
    {
    }
}