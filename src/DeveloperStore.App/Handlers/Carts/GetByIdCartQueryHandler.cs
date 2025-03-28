using AutoMapper;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Repositories;
using MediatR;
using DeveloperStore.App.Models.Queries.Cart.Request;
using DeveloperStore.App.Models.Queries.Cart.Response;

namespace DeveloperStore.App.Handlers.Carts
{
    public class GetByIdCartQueryHandler(
        ICartRepository cartRepository,
        IMapper mapper) : IRequestHandler<GetByIdCartQueryRequest, GetByIdCartQueryResponse>
    {
        public async Task<GetByIdCartQueryResponse> Handle(GetByIdCartQueryRequest request, CancellationToken cancellationToken)
        {
            var options = new QueryOptions
            {
                IsAsNoTracking = true
            };

            var result = await cartRepository.GetByIdAsync(request.Id, options);

            return mapper.Map<GetByIdCartQueryResponse>(result);
        }
    }
}
