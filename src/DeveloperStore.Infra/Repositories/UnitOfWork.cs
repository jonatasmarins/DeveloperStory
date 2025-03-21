using DeveloperStore.Domain.Repositories;
using DeveloperStore.Infra.Context;

namespace DeveloperStore.Infra.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public AppDbContext Context { get; private set; }

        public UnitOfWork(AppDbContext context)
        {
            Context = context;
        }

        private bool disposed = false;


        public async Task SaveAsync(CancellationToken cancellationToken)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
