using AutoMapper;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Repositories;
using MediatR;
using DeveloperStore.App.Models.Commands.Cart.Request;
using DeveloperStore.App.Models;
using DeveloperStore.App.Models.Commands.Cart.Response;

namespace DeveloperStore.App.Handlers.Carts
{
    public class DeleteCartCommandHandler(
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<DeleteCartCommandRequest, IResultResponse<DeleteCartCommandResponse>>
    {
        public async Task<IResultResponse<DeleteCartCommandResponse>> Handle(DeleteCartCommandRequest request, CancellationToken cancellationToken)
        {
            var response = new ResultResponse<DeleteCartCommandResponse>()
            {
                Data = new DeleteCartCommandResponse("Product deleted with Success !")
            };

            var product = await cartRepository.GetByIdAsync(request.Id, new QueryOptions { IsAsNoTracking = true, IsIgnoreAutoIncludes = true });

            if (product == null || product.Id == 0)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.AddMessage("Produt not found!");

                return response;
            }                

            await cartRepository.DeleteAsync(request.Id);

            await unitOfWork.SaveAsync(cancellationToken);

            return response;
        }
    }
}
