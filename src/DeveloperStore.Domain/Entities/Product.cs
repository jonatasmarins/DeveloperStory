namespace DeveloperStore.Domain.Entities
{
    public class Product : Entity
    {
        public Product()
        {
            Rating = new Rating();   
        }

        public string Title { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Image { get; set; } 
        public Rating Rating { get; set; }
    }
}
