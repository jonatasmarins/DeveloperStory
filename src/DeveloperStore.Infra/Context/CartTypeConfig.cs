using DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

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

            //builder
            //    .Property(p => p.UserId)
            //    .IsRequired();

            //builder
            //.HasIndex(x => x.UserId)
            //.IsUnique();

            //builder
            //.HasOne(x => x.User)
            //.WithMany()
            //.HasForeignKey(x => x.UserId);

            //builder
            //.HasMany(e => e.Products)
            //.WithOne()
            //.HasForeignKey(e => e.CartId)
            //.IsRequired();

            //builder
            //    .Navigation(a => a.Products)
            //    .AutoInclude(true);
        }
    }
}
