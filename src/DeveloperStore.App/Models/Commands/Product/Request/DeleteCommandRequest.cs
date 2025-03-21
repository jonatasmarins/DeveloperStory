using DeveloperStore.App.Models.Commands.Product.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace DeveloperStore.App.Models.Commands.Product.Request
{
    public class DeleteCommandRequest : IRequest<DeleteCommandResponse>
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
