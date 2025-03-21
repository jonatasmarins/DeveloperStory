using DeveloperStore.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DeveloperStore.Tests.Repositories
{
    public class RepositoryHelper(ServiceProvider Provider)
    {
        public async Task<T> ExecuteAsync<TRepository, T>(Func<TRepository, Task<T>> action)
        {
            using var scope = Provider.CreateScope();

            var productRepository = scope.ServiceProvider.GetRequiredService<TRepository>();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            var result = await action(productRepository).ConfigureAwait(false);

            await unitOfWork.SaveAsync(CancellationToken.None);

            return result;
        }

        public async Task ExecuteAsync<TRepository>(Func<TRepository, Task> action)
        {
            using var scope = Provider.CreateScope();

            var productRepository = scope.ServiceProvider.GetRequiredService<TRepository>();

            var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

            await action(productRepository).ConfigureAwait(false);

            await unitOfWork.SaveAsync(CancellationToken.None);
        }
    }
}
