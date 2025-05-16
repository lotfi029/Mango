using Store.Services.AuthAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Store.Services.AuthAPI.Persistence;

public class AppDbContext(
    DbContextOptions<AppDbContext> options
    ) : IdentityDbContext<AppUser, AppRole, string>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
