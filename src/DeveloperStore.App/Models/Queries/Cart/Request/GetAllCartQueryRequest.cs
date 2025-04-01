using DeveloperStore.App.Models.Queries.Cart.Response;
using DeveloperStore.App.Models;
using MediatR;

namespace DeveloperStore.App.Models.Queries.Cart.Request
{
    public class GetAllCartQueryRequest : PaginatedQueryRequest, IRequest<IResultResponse<IEnumerable<GetAllCartQueryResponse>>>
    {
        public GetAllCartQueryRequest() : base(string.Empty, 1, 10)
        {

        }

        public GetAllCartQueryRequest(string order, int page = 1, int size = 10) : base(order, page, size) { }
    }
}


