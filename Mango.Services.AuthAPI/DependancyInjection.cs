using Carter;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mango.Services.AuthAPI.Authentication;
using Mango.Services.AuthAPI.Entities;
using Mango.Services.AuthAPI.HostedServices;
using Mango.Services.AuthAPI.Persistence;
using Mango.Services.AuthAPI.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Mango.Services.AuthAPI.Options;

namespace Mango.Services.AuthAPI;

public static class DependancyInjection
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddOpenApi();
        services.AddServices();
        services.AddCarter();

        services.AddAuthService(configuration);
        services.InjectService(configuration);


        services.AddDbServices(configuration);
        services.AddHostedService<MigrationService>();
        return services;
    }
    private static IServiceCollection InjectService(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddScoped<IEmailSender, EmailService>();
        services.AddOptions<MailOptions>()
            .BindConfiguration(MailOptions.SectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();


        return services;
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddOpenApi();
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(config));

        

        return services;
    }
    private static IServiceCollection AddAuthService(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddAuthorization();

        services.AddSingleton<IJwtProvider, JwtProvider>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();

        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
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

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        });


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
