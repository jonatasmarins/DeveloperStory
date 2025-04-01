using DeveloperStore.App.Models.Commands.Cart.Response;
using DeveloperStore.App.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace DeveloperStore.App.Models.Commands.Cart.Request
{
    public class UpdateCartCommandRequest : IRequest<IResultResponse<UpdateCartCommandResponse>>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<UpdateCartProductQueryRequest> Products { get; set; } = [];

        public class UpdateCartProductQueryRequest
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
