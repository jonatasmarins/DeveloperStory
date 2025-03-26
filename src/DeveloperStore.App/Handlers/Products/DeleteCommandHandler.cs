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
        IMapper mapper) : IRequestHandler<DeleteProductCommandRequest, DeleteProductCommandResponse>
    {
        public async Task<DeleteProductCommandResponse> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdAsync(request.Id, new QueryOptions { IsAsNoTracking = true, IsIgnoreAutoIncludes = true });

            if (product == null || product.Id == 0) return new DeleteProductCommandResponse { Message = "Produt not found!", IsSuccess = false };

            await productRepository.DeleteAsync(request.Id);

            await unitOfWork.SaveAsync(cancellationToken);

            return new DeleteProductCommandResponse { Message = "Product deleted with Success !", IsSuccess = true};
        }
    }
}
