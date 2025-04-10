using Carter;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mango.Services.CouponAPI.Abstracts.Constants;
using Mango.Services.CouponAPI.HostedServices;
using Mango.Services.CouponAPI.Persistence;
using Mango.Services.CouponAPI.Repositories;
using Mango.Services.CouponAPI.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

namespace Mango.Services.CouponAPI;

public static class DependancyInjection
{
    public static IServiceCollection AddCouponServices(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddServices();
        services.RegisterServicesAndRespository();
        services.AddAuthConfig(configuration);


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
    private static IServiceCollection AddAuthConfig(this IServiceCollection services, IConfigurationManager configuration)
    {

        services.AddAuthorization();

        services.AddOptions<JwtOptions>()
           .BindConfiguration(JwtOptions.SectionName)
           .ValidateDataAnnotations()
           .ValidateOnStart();

        var settings = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.SaveToken = true;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings!.Key)),
                ValidIssuer = settings.Issuer,
                ValidAudience = settings.Audience,
            };
        });

        services.AddAuthorizationBuilder()
            .AddPolicy(DefaultRoles.User, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole(DefaultRoles.User);
            })
            .AddPolicy(DefaultRoles.Admin, policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireRole(DefaultRoles.Admin);
            });

        return services;
    }
}
