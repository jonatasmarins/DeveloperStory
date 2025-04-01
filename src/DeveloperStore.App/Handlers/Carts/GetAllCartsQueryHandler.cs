using AutoMapper;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.App.Models;
using MediatR;
using DeveloperStore.App.Models.Queries.Cart.Request;
using DeveloperStore.App.Models.Queries.Cart.Response;

namespace DeveloperStore.App.Handlers.Carts
{
    public class GetAllCartsQueryHandler(
        ICartRepository cartRepository,
        IMapper mapper
    ) : IRequestHandler<GetAllCartQueryRequest, IResultResponse<IEnumerable<GetAllCartQueryResponse>>>
    {

        public async Task<IResultResponse<IEnumerable<GetAllCartQueryResponse>>> Handle(GetAllCartQueryRequest request, CancellationToken cancellationToken)
        {
            var options = new QueryOptions
            {
                IsAsNoTracking = true,
                Page = request.Page,
                Size = request.Size,
                Order = request.Order,
            };

            var result = await cartRepository.GetAllAsync(options);

            return mapper.Map<ResultResponse<IEnumerable<GetAllCartQueryResponse>>>(result);
        }
    }
}
