using DeveloperStore.App.Models.Queries.Product.Responses;

namespace DeveloperStore.App.Models.Commands.Product.Response
{
    public class AddCommandResponse
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
