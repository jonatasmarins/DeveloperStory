using DeveloperStore.Infra.Context.Identity;

namespace DeveloperStore.Domain.Entities
{
    public class Cart : Entity
    {
        public DateTime Date { get; set; }                
        public int UserId { get; set; }
        //public ApplicationUser User { get; set; } = null!;
        //public virtual ICollection<CartProduct> Products { get; set; } = [];

    }

    public class CartProduct
    { 
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
