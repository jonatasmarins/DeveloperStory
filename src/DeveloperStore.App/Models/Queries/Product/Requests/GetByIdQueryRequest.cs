using DeveloperStore.App.Models.Queries.Product.Responses;
using MediatR;

namespace DeveloperStore.App.Models.Queries.Product.Requests
{
    public class GetByIdQueryRequest() : IRequest<IResultResponse<GetByIdQueryResponse>>
    {
        public int Id { get; set; }
    }
}
