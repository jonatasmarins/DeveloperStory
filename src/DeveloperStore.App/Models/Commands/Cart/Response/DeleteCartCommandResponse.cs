namespace DeveloperStore.App.Models.Commands.Cart.Response
{
    public class DeleteCartCommandResponse(string message)
    {
        public string Message { get; set; } = message;
    }
}
