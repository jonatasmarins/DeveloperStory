namespace DeveloperStore.App.Models.Commands.Cart.Response
{
    public class UpdateCartCommandResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<UpdateCartProductCommandResponse> Products { get; set; } = [];

        public class UpdateCartProductCommandResponse
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
