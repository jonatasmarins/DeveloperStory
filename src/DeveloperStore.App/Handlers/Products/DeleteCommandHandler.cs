using AutoMapper;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Commands.Product.Response;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class DeleteCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<DeleteCommandRequest, DeleteCommandResponse>
    {
        public async Task<DeleteCommandResponse> Handle(DeleteCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id, new QueryOptions { IsAsNoTracking = true, IsIgnoreAutoIncludes = true });

            if (product == null || product.Id == 0) return new DeleteCommandResponse { Message = "Produt not found!", IsSuccess = false };

            await productRepository.DeleteAsync(request.Id);

            await unitOfWork.SaveAsync(cancellationToken);

            return new DeleteCommandResponse { Message = "Product deleted with Success !", IsSuccess = true};
        }
    }
}
