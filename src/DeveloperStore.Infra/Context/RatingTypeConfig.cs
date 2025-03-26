using DeveloperStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperStore.Infra.Context
{
    internal class RatingTypeConfig : IEntityTypeConfiguration<Rating>
    {
        public void Configure(EntityTypeBuilder<Rating> builder)
        {

            builder
                .HasKey(p => p.Id)
                .HasName("PK_RATING");            

            builder
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            builder
                .Property(p => p.ProductId)
                .IsRequired();

            builder
                .Property(p => p.Count)
                .HasDefaultValue(0);                
        }
    }
}
