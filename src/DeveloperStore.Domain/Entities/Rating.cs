namespace DeveloperStore.Domain.Entities
{
    public class Rating : Entity
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }
        public int ProductId { get; set; }
    }
}
