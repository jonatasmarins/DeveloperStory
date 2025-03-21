using DeveloperStore.Domain.Services.Models;
using MediatR;

namespace DeveloperStore.App.Models.Queries.Product.Requests
{
    public class GetAllCategoriesQueryRequest : IRequest<IEnumerable<string>>
    {        
    }
}
