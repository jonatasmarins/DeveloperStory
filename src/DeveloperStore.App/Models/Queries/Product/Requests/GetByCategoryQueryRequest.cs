using DeveloperStore.App.Models.Queries.Product.Responses;
using DeveloperStore.Domain.Services.Models;
using MediatR;

namespace DeveloperStore.App.Models.Queries.Product.Requests
{
    public class GetByCategoryQueryRequest : PaginatedQueryRequest, IRequest<IResultResponse<IEnumerable<GetByCategoryQueryResponse>>>
    {
        public string Category { get; set; } = string.Empty;

        public GetByCategoryQueryRequest() : base(string.Empty, 1, 10)
        {

        }

        public GetByCategoryQueryRequest(string order, int page = 1, int size = 10) : base(order, page, size) { }
    }
}

