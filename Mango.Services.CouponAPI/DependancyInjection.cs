using Carter;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mango.Services.CouponAPI.HostedServices;
using Mango.Services.CouponAPI.Persistence;
using Mango.Services.CouponAPI.Repositories;
using Mango.Services.CouponAPI.Services;
using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Mango.Services.CouponAPI;

public static class DependancyInjection
{
    public static IServiceCollection AddCouponServices(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddServices();
        services.RegisterServicesAndRespository();

        services.AddDbServices(configuration);
        services.AddHostedService<MigrationService>();
        return services;
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddCarter();
        services.AddOpenApi();
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(config));

        

        return services;
    }
    private static IServiceCollection RegisterServicesAndRespository(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ICouponService, CouponService>();

        return services;
    }

    private static IServiceCollection AddDbServices(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"))
            );
        
        return services;
    }
}
