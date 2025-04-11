using FluentValidation.AspNetCore;
using Mango.Web.Abstracts;
using Mango.Web.Contracts;
using Mango.Web.Services;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Mango.Web;

public static class DependancyInjection
{
    public static IServiceCollection AddMangoWebService(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.RegisterServicesAndRespository(configuration);
        services.AddServices();
        return services;
    }
    private static IServiceCollection RegisterServicesAndRespository(this IServiceCollection services, IConfigurationManager configuration)
    {
        services.AddHttpClient();
        services.AddHttpContextAccessor();
        services.AddHttpClient<ICouponService, CouponService>();

        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<ICouponService, CouponService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenProvider, TokenProvider>();

        services.AddOptions<ApiSettings>()
            .BindConfiguration(ApiSettings.Section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.LoginPath = "/auth/login";
                options.AccessDeniedPath = "/auth/forbidden";
                options.ExpireTimeSpan = TimeSpan.FromHours(10);
            });

        return services;
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(typeof(CouponRequestValidator).Assembly);

        return services;
    }
}
