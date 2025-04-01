using AutoMapper;
using DeveloperStore.App.Models.Commands.Cart.Request;
using DeveloperStore.App.Models.Commands.Cart.Response;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.App.Models;
using MediatR;

namespace DeveloperStore.App.Handlers.Carts
{
    public class UpdateCartCommandHandler(
        ICartRepository cartRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper) : IRequestHandler<UpdateCartCommandRequest, IResultResponse<UpdateCartCommandResponse>>
    {
        public async Task<IResultResponse<UpdateCartCommandResponse>> Handle(UpdateCartCommandRequest request, CancellationToken cancellationToken)
        {
            //TODO FLuent Validation
            var response = new ResultResponse<UpdateCartCommandResponse>();

            var entity = await cartRepository.GetByIdAsync(request.Id, new Domain.Repositories.Models.QueryOptions { IsAsNoTracking = true });

            if (entity == null || entity.Id == 0)
            {
                response.AddMessage("Cart Not Found !");

                return response;
            }

            mapper.Map(request, entity);

            var result = await cartRepository.UpdateAsync(entity);

            await unitOfWork.SaveAsync(cancellationToken);

            response.Data = mapper.Map<UpdateCartCommandResponse>(result);

            return response;
        }
    }
}
