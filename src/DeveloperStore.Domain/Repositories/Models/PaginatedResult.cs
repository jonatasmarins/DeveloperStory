namespace DeveloperStore.Domain.Repositories.Models
{
    public class PaginatedResult<T> where T : class
    {
        public required T Data { get; set; } = default;
        public int TotalItems { get; set; } = 0;
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);

    }
}
