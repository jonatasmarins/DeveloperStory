using DeveloperStore.App.Models;
using DeveloperStore.App.Models.Commands.Product.Request;
using DeveloperStore.App.Models.Commands.Product.Response;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class DeleteCommandHandler(
        IProductRepository productRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductCommandRequest, IResultResponse<DeleteProductCommandResponse>>
    {
        public async Task<IResultResponse<DeleteProductCommandResponse>> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ResultResponse<DeleteProductCommandResponse>();

            var entity = await productRepository.GetByIdAsync(request.Id, new QueryOptions { IsAsNoTracking = true, IsIgnoreAutoIncludes = true });

            if (entity == null || entity.Id == 0)
            {
                response.AddMessage($"The Product with ID {request.Id} does not exist in our database");

                response.StatusCode = System.Net.HttpStatusCode.NotFound;

                return response;
            }

            await productRepository.DeleteAsync(request.Id);

            await unitOfWork.SaveAsync(cancellationToken);

            response.Data = new DeleteProductCommandResponse() { Message = $"Product {entity.Title} with ID {request.Id} deleted with Success !" };

            return response;
        }
    }
}
