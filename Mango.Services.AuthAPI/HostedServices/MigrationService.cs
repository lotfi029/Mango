
using Store.Services.AuthAPI.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Store.Services.AuthAPI.HostedServices;

public class MigrationService(IServiceProvider _serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        if (dbContext.Database.GetPendingMigrations().Any())
            await dbContext.Database.MigrateAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}
