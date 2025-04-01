using DeveloperStore.App.Models.Commands.User.Response;
using DeveloperStore.App.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace DeveloperStore.App.Models.Commands.User.Request
{
    public class DeleteUserCommandRequest : IRequest<IResultResponse<DeleteUserCommandResponse>>
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }
}
