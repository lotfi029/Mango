using Mango.Services.AuthAPI.Abstracts.Constants;
using Mango.Services.AuthAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mango.Services.AuthAPI.Persistence.Configurations;

public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
{
    public void Configure(EntityTypeBuilder<AppRole> builder)
    {
        builder.Property(e => e.IsDisable)
            .HasDefaultValue(false);

        var adminRole = new AppRole
        {
            Id = DefaultRoles.AdminId,
            Name = DefaultRoles.AdminName,
            ConcurrencyStamp = DefaultRoles.ConcurrencyStamp,
            IsDisable = false,
            NormalizedName = DefaultRoles.AdminName.ToUpper()
        };

        builder.HasData(adminRole);
    }
}
