using AutoMapper;
using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using DeveloperStore.Domain.Services.Models;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class GetByCategoryQueryHandler(
        IProductRepository productRepository,
        IMapper mapper
    ) : IRequestHandler<GetByCategoryQueryRequest, IResultResponse<IEnumerable<GetByCategoryQueryResponse>>>
    {
        public async Task<IResultResponse<IEnumerable<GetByCategoryQueryResponse>>> Handle(GetByCategoryQueryRequest request, CancellationToken cancellationToken)
        {
            var options = new QueryOptions
            {
                IsAsNoTracking = true,
                Page = request.Page,
                Size = request.Size,
                Order = request.Order,
            };

            var result = await productRepository.GetByCategory(request.Category, options);

            return mapper.Map<ResultResponse<IEnumerable<GetByCategoryQueryResponse>>>(result);
        }
    }
}
