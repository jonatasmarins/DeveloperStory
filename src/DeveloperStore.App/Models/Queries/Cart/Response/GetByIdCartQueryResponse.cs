namespace DeveloperStore.App.Models.Queries.Cart.Response
{
    public class GetByIdCartQueryResponse
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public List<GetBydIdCartProductQueryResponse> Products { get; set; } = [];

        public class GetBydIdCartProductQueryResponse
        {
            public int ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
