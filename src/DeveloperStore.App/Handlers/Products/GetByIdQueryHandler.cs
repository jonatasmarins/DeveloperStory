using AutoMapper;
using DeveloperStore.App.Models;
using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.Domain.Repositories;
using DeveloperStore.Domain.Repositories.Models;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class GetByIdQueryHandler(
        IProductRepository productRepository,
        IMapper mapper) : IRequestHandler<GetByIdQueryRequest, IResultResponse<GetByIdQueryResponse>>
    {
        public async Task<IResultResponse<GetByIdQueryResponse>> Handle(GetByIdQueryRequest request, CancellationToken cancellationToken)
        {

            var response = new ResultResponse<GetByIdQueryResponse>();

            var options = new QueryOptions
            {
                IsAsNoTracking = true
            };

            var result = await productRepository.GetByIdAsync(request.Id, options);

            if (result is null || result.Id == 0)
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.AddMessage($"Product with {request.Id} ID does not exist in our database");
            }

            response.Data = mapper.Map<GetByIdQueryResponse>(result);

            return response;
        }
    }
}
