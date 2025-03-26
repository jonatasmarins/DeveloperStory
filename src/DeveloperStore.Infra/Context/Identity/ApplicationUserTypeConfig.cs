using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperStore.Infra.Context.Identity
{
    public class ApplicationUserTypeConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .HasKey(p => p.UserId)
                .HasName("PK_USERID");

            builder
                .Property(p => p.UserId)
                .ValueGeneratedOnAdd();

            builder
                .OwnsOne(u => u.Name);

            builder
                .OwnsOne(u => u.Address, a =>
                {
                    a.OwnsOne(addr => addr.Geolocation);
                });
        }
    }
}
