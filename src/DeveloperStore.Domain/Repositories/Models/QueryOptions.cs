namespace DeveloperStore.Domain.Repositories.Models
{
    public class QueryOptions
    {
        public bool IsAsNoTracking { get; set; } = false;
        public bool IsIgnoreAutoIncludes { get; set; } = false;
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string Order { get; set; } = string.Empty;
    }
}
