using Store.Services.AuthAPI.Abstracts.Constants;
using Store.Services.AuthAPI.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Store.Services.AuthAPI.Persistence.Configurations;

public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
{
    public void Configure(EntityTypeBuilder<AppUser> builder)
    {
        builder.Property(e => e.IsDisable)
            .HasDefaultValue(false);

        builder.Property(e => e.FirstName)
            .HasMaxLength(100);
        
        builder.Property(e => e.LastName)
            .HasMaxLength(100);

        var adminUser = new AppUser
        {
            FirstName = DefaultUsers.FirstName,
            LastName = DefaultUsers.LastName,
            Email = DefaultUsers.Email,
            Id = DefaultUsers.Id,
            ConcurrencyStamp = DefaultUsers.ConcurrencyStamp,
            SecurityStamp = DefaultUsers.SecurityStamp,
            IsDisable = false,
            EmailConfirmed = true,
            NormalizedEmail = DefaultUsers.Email.ToUpper(),
            NormalizedUserName = DefaultUsers.Email.ToUpper(),
            UserName = DefaultUsers.Email,
            PasswordHash = "AQAAAAIAAYagAAAAEFvHrdpAbHYwwvFgy0vSi++IVSQG3EvXG6qJtsPhvPM5ae2n8gvbO3hdM0ReuTgZnw==",
        };


        builder.HasData(adminUser);
    }
}
