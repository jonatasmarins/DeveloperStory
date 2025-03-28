namespace DeveloperStore.App.Models.Commands.Cart.Response
{
    public class AddCartCommandResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<AddCartProductQueryResponse> Products { get; set; } = [];

        public class AddCartProductQueryResponse
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
