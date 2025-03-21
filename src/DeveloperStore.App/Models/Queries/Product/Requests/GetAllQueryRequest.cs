using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.Domain.Services.Models;
using MediatR;

namespace DeveloperStore.App.Models.Queries.Product.Requests
{
    public class GetAllQueryRequest : PaginatedQueryRequest, IRequest<IResultResponse<IEnumerable<GetProductsQueryResponse>>>
    {
        public GetAllQueryRequest() : base(string.Empty, 1, 10)
        {
            
        }

        public GetAllQueryRequest(string order, int page = 1, int size = 10) : base(order, page, size) { }
    }
}
