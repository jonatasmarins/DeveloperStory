using AutoMapper;
using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class GetByIdQueryHandler(
        IProductRepository productRepository,
        IMapper mapper) : IRequestHandler<GetByIdQueryRequest, GetByIdQueryResponse>
    {
        public async Task<GetByIdQueryResponse> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var options = new QueryOptions
            {
                IsAsNoTracking = true            
            };

            var result = await productRepository.GetByIdAsync(request.Id, options);

            return mapper.Map<GetByIdQueryResponse>(result);
        }
    }
}
