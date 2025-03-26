using DeveloperStore.App.Models.Commands.Product.Response;
using DeveloperStore.App.Models.Queries.Product.Responses;
using MediatR;

namespace DeveloperStore.App.Models.Commands.Product.Request
{
    public class AddProductCommandRequest : IRequest<AddProductCommandResponse>
    {        
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        public GetRatingQueryResponse Rating { get; set; }
    }
}
