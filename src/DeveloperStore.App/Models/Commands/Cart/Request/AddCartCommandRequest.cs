using DeveloperStore.App.Models.Commands.Cart.Response;
using DeveloperStore.App.Models;
using MediatR;

namespace DeveloperStore.App.Models.Commands.Cart.Request
{
    public class AddCartCommandRequest : IRequest<IResultResponse<AddCartCommandResponse>>
    {        
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<AddCartProductQueryRequest> Products { get; set; } = [];

        public class AddCartProductQueryRequest
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
