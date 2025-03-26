using DeveloperStore.App.Models.Commands.Product.Response;
using DeveloperStore.App.Models.Queries.Product.Responses;
using MediatR;
using System.Text.Json.Serialization;

namespace DeveloperStore.App.Models.Commands.Product.Request
{
    public class UpdateProductCommandRequest : IRequest<UpdateProductCommandResponse>
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        public GetRatingQueryResponse Rating { get; set; }
    }
}
