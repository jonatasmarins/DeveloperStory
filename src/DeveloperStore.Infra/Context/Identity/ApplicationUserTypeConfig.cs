using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperStore.Infra.Context.Identity
{
    public class ApplicationUserTypeConfig : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
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
