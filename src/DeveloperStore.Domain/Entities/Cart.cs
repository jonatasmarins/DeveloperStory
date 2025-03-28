using DeveloperStore.Infra.Context.Identity;
using System.ComponentModel.DataAnnotations;

namespace DeveloperStore.Domain.Entities
{
    public class Cart : Entity
    {
        public DateTime Date { get; set; }                
        public int UserId { get; set; }
        public virtual ICollection<CartProduct> Products { get; set; } = [];
    }

    public class CartProduct
    {
        [Key]
        public int CartId { get; set; }

        [Key]
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
