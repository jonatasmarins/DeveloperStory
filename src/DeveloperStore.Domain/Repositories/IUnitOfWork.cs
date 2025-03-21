using System;

namespace DeveloperStore.Domain.Repositories
{
    public interface IUnitOfWork
    {        
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
