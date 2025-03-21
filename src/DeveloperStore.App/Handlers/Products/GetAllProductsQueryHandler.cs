using AutoMapper;
using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Services.Models;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class GetAllProductsQueryHandler(
        IProductRepository productRepository,
        IMapper mapper
    ) : IRequestHandler<GetAllQueryRequest, IResultResponse<IEnumerable<GetProductsQueryResponse>>>
    {

        public async Task<IResultResponse<IEnumerable<GetProductsQueryResponse>>> Handle(GetAllQueryRequest request, CancellationToken cancellationToken)
        {            
            var options = new QueryOptions
            {
                IsAsNoTracking = true,
                Page = request.Page,
                Size = request.Size,
                Order = request.Order,
            };

            var result = await productRepository.GetAllAsync(options);

            return mapper.Map<ResultResponse<IEnumerable<GetProductsQueryResponse>>>(result);
        }
    }
}
