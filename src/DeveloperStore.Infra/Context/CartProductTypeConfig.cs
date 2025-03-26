using DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace DeveloperStore.Infra.Context
{
    public class CartProductTypeConfig : IEntityTypeConfiguration<CartProduct>
    {
        public void Configure(EntityTypeBuilder<CartProduct> builder)
        {
            builder
                .HasKey(op => new { op.CartId, op.ProductId })
                .HasName("PK_MULT_CARTID_PRODID");
        }
    }
}
