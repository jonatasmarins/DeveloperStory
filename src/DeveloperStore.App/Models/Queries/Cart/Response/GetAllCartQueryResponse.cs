namespace DeveloperStore.App.Models.Queries.Cart.Response
{
    public class GetAllCartQueryResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<GetCartProductQueryResponse> Products { get; set; } = [];

        public class GetCartProductQueryResponse
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
