using System.Text.Json.Serialization;

namespace DeveloperStore.App.Models.Commands.Product.Response
{
    public class DeleteCommandResponse
    {
        [JsonIgnore]
        public bool IsSuccess { get; set; } = false;
        public string Message { get; set; } = string.Empty;
    }
}
