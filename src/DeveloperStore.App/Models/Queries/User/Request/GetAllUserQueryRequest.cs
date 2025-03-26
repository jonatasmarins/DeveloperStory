using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.App.Models.Queries.User.Response;
using DeveloperStore.Domain.Services.Models;
using MediatR;

namespace DeveloperStore.App.Models.Queries.User.Request
{
    public class GetAllUserQueryRequest : PaginatedQueryRequest, IRequest<IResultResponse<IEnumerable<GetAllUserQueryResponse>>>
    {
        public GetAllUserQueryRequest() : base(string.Empty, 1, 10)
        {

        }

        public GetAllUserQueryRequest(string order, int page = 1, int size = 10) : base(order, page, size) { }
    }
}

