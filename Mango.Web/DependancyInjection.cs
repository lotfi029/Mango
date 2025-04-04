﻿using FluentValidation;
using FluentValidation.AspNetCore;
using Mango.Web.Contracts;
using Mango.Web.Service;
using Mango.Web.Service.IService;

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

        services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
        services.AddScoped<ICouponService, CouponService>();

        services.AddOptions<ApiSettings>()
            .BindConfiguration(ApiSettings.Section)
            .ValidateDataAnnotations()
            .ValidateOnStart();

        return services;
    }
    private static IServiceCollection AddServices(this IServiceCollection services)
    {

        services.AddFluentValidationAutoValidation()
            .AddValidatorsFromAssembly(typeof(CouponRequestValidator).Assembly);

        return services;
    }
}
