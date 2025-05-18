using Microsoft.EntityFrameworkCore;
using Store.Services.ShoppingCartAPI.Entities;

namespace Store.Services.ShoppingCartAPI.Presistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<CartDetails> CartDetails { get; set; }
    public DbSet<CartHeader> CartHeaders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
