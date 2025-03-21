namespace DeveloperStore.App.Models.Queries
{
    public abstract class PaginatedQueryRequest
    {
        public PaginatedQueryRequest(string order, int page = 1, int size = 10)
        {
            Order = order;
            Page = page;
            Size = size;
        }

        public int Page { get; set; }
        public int Size { get; set; }
        public string Order { get; set; } = string.Empty;
    }
}
