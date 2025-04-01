using DeveloperStore.App.Models.Commands.Cart.Response;
using DeveloperStore.App.Models;
using MediatR;

namespace DeveloperStore.App.Models.Commands.Cart.Request
{
    public class DeleteCartCommandRequest : IRequest<IResultResponse<DeleteCartCommandResponse>>
    {
        public int Id { get; set; }
    }
}
