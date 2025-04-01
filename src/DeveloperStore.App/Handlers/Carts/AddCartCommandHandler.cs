using AutoMapper;
using DeveloperStore.App.Models.Commands.Cart.Request;
using DeveloperStore.App.Models.Commands.Cart.Response;
using DeveloperStore.Domain.Entities;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.App.Models;
using MediatR;

namespace DeveloperStore.App.Handlers.Carts
{
    public class AddCartCommandHandler(
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<AddCartCommandRequest, IResultResponse<AddCartCommandResponse>>
    {
        public async Task<IResultResponse<AddCartCommandResponse>> Handle(AddCartCommandRequest request, CancellationToken cancellationToken)
        {
            //TODO FLuent Validation
            var response = new ResultResponse<AddCartCommandResponse>();

            var entity = mapper.Map<Cart>(request);

            var result = await cartRepository.AddAsync(entity);

            await unitOfWork.SaveAsync(cancellationToken);

            response.Data = mapper.Map<AddCartCommandResponse>(result);

            return response;
        }
    }
}
