using System.Text.Json.Serialization;

namespace DeveloperStore.App.Models.Commands.Product.Response
{
    public class DeleteProductCommandResponse
    {
        public string Message { get; set; } = string.Empty;
    }
}
