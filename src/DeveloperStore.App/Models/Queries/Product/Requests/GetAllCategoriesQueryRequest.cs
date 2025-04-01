using DeveloperStore.App.Models;
using MediatR;

namespace DeveloperStore.App.Models.Queries.Product.Requests
{
    public class GetAllCategoriesQueryRequest : IRequest<IResultResponse<IEnumerable<string>>>
    {        
    }
}
