using DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace DeveloperStore.Infra.Context
{
    public class ProductTypeConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder
                .HasKey(p => p.Id)
                .HasName("PK_PRODUCTID");

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Price)
                .HasPrecision(14, 2)
                .IsRequired();

            builder.Property(p => p.Description)
                .IsRequired();

            builder.Property(p => p.Category)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Image)
                .IsRequired();

            builder
                .HasOne(p => p.Rating)
                .WithOne()
                .HasForeignKey<Rating>(p => p.ProductId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();

            builder
                .Navigation(a => a.Rating)
                .AutoInclude(true);
        }
    }
}
