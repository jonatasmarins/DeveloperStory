using DeveloperStore.App.Models.Queries.Product.Requests;
using DeveloperStore.Domain.Repositories;
using MediatR;

namespace DeveloperStore.App.Handlers.Products
{
    public class GetAllCategoriesQueryHandler(
        IProductRepository productRepository        
    ) : IRequestHandler<GetAllCategoriesQueryRequest, IEnumerable<string>>
    {
        public async Task<IEnumerable<string>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
        {            
            return await productRepository.GetAllCategories();
        }
    }
}
