using DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using DeveloperStore.Infra.Context.Identity;

namespace DeveloperStore.Infra.Context
{
    public class CartTypeConfig : IEntityTypeConfiguration<Cart>
    {
        public void Configure(EntityTypeBuilder<Cart> builder)
        {
            builder
                .HasKey(p => p.Id)
                .HasName("PK_CARTID");

            builder
            .Property(p => p.Id)
            .ValueGeneratedOnAdd();

            builder
            .HasOne<ApplicationUser>()
            .WithMany(x => x.Carts)
            .HasForeignKey(x => x.UserId);

            builder
                .Navigation(a => a.Products)
                .AutoInclude(true);
        }
    }
}
