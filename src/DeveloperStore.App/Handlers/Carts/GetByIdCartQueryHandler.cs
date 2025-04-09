using AutoMapper;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Repositories;
using MediatR;
using DeveloperStore.App.Models.Queries.Cart.Request;
using DeveloperStore.App.Models.Queries.Cart.Response;
using DeveloperStore.App.Models;

namespace DeveloperStore.App.Handlers.Carts
{
    public class GetByIdCartQueryHandler(
        ICartRepository cartRepository,
        IMapper mapper) : IRequestHandler<GetByIdCartQueryRequest, IResultResponse<GetByIdCartQueryResponse>>
    {
        public async Task<IResultResponse<GetByIdCartQueryResponse>> Handle(GetByIdCartQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResultResponse<GetByIdCartQueryResponse>();

            var options = new QueryOptions
            {
                IsAsNoTracking = true
            };

            var result = await cartRepository.GetByIdAsync(request.Id, options);

            if (result is null || result.Id == 0)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.AddMessage($"Cart with {request.Id} ID does not exist in our database");
            }

            response.Data = mapper.Map<GetByIdCartQueryResponse>(result);

            return response;
        }
    }
}
