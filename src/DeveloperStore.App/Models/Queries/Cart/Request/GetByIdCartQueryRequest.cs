using DeveloperStore.App.Models.Queries.Cart.Response;
using MediatR;

namespace DeveloperStore.App.Models.Queries.Cart.Request
{
    public class GetByIdCartQueryRequest : IRequest<IResultResponse<GetByIdCartQueryResponse>>
    {
        public int Id { get; set; }
    }
}