namespace DeveloperStore.App.Models.Queries.Product.Responses
{
    public class GetByCategoryQueryResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; }

        public GetRatingQueryResponse Rating { get; set; }
    }
}
