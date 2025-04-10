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

        AppRole[] roles = [
            new AppRole {
                Id = DefaultRoles.AdminId,
                Name = DefaultRoles.AdminName,
                ConcurrencyStamp = DefaultRoles.ConcurrencyStamp,
                IsDisable = false,
                NormalizedName = DefaultRoles.AdminName.ToUpper()
            },
            new AppRole {
                Id = DefaultRoles.UserId,
                Name = DefaultRoles.UserName,
                ConcurrencyStamp = DefaultRoles.UserConcurrencyStamp,
                IsDisable = false,
                NormalizedName = DefaultRoles.UserName.ToUpper()
            }
        ];

        builder.HasData(roles);
    }
}
