using Microsoft.EntityFrameworkCore;
using Store.Services.ShoppingCartAPI.Presistence;

namespace Store.Services.ShoppingCartAPI.HostedServices;

public class MigrationService(IServiceProvider _serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (dbContext.Database.GetPendingMigrations().Any())
            await dbContext.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
