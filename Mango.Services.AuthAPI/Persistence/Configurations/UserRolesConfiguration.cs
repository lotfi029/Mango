using Store.Services.AuthAPI.Abstracts.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Services.AuthAPI.Persistence.Configurations;

public class UserRolesConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {
        var adminUser = new IdentityUserRole<string>
        {
            RoleId = DefaultRoles.AdminId,
            UserId = DefaultUsers.Id
        };

        builder.HasData(adminUser);
    }
}
