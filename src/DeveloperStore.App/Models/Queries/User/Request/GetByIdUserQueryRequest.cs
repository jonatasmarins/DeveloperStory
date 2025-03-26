using DeveloperStore.App.Models.Queries.User.Response;
using MediatR;

namespace DeveloperStore.App.Models.Queries.User.Request
{
    public class GetByIdUserQueryRequest : IRequest<GetByIdUserQueryResponse>
    {
        public int Id { get; set; }
    }
}
