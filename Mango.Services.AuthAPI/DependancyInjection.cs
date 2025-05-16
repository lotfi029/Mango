using Carter;
using FluentValidation;
using FluentValidation.AspNetCore;
using Store.Services.AuthAPI.Authentication;
using Store.Services.AuthAPI.Entities;
using Store.Services.AuthAPI.HostedServices;
using Store.Services.AuthAPI.Persistence;
using Store.Services.AuthAPI.Services;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity.UI.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;
using Store.Services.AuthAPI.Options;
using Store.Services.AuthAPI.Services;

namespace Store.Services.AuthAPI;

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
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IFileService, FileService>();

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
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789!@#$-_";
            options.User.RequireUniqueEmail = true;
            options.SignIn.RequireConfirmedEmail = true;
        });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
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
