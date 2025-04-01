using DeveloperStore.App.Models;
using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.Domain.Repositories;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class GetAllCategoriesQueryHandler(
        IProductRepository productRepository        
    ) : IRequestHandler<GetAllCategoriesQueryRequest, IResultResponse<IEnumerable<string>>>
    {
        public async Task<IResultResponse<IEnumerable<string>>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {
            var response = new ResultResponse<IEnumerable<string>>();

            var result = await productRepository.GetAllCategories();

            if (result is null || !result.Any())
            {
                response.StatusCode = System.Net.HttpStatusCode.NotFound;

                response.Data = [];

                return response;
            }

            response.Data = result;

            return response;
        }
    }
}
