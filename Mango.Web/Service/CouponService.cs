using Mango.Web.Abstracts;
using Mango.Web.Contracts;
using Mango.Web.Service.IService;
using Microsoft.Extensions.Options;

namespace Mango.Web.Service;

public class CouponService(
    IBaseService<CouponResponse> baseService,
    IOptions<ApiSettings> options) : ICouponService
{
    private readonly ApiSettings _options = options.Value;

    public async Task<Result> CreateCoupon(CouponRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.CouponAPI + "/api/coupons",
            "non token",
            ApiType.POST,
            request
        ));
    }
    public async Task<Result> UpdateCoupon(int id, CouponRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.CouponAPI + $"/api/coupons/{id}",
            "non token",
            ApiType.PUT,
            request
        ));
    }
    public async Task<Result> DeleteCoupon(int id, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.CouponAPI + $"/api/coupons/{id}",
            "non token",
            ApiType.DELETE,
            null!
        ));
    }

    public async Task<Result> GetAllCoupons(CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.CouponAPI + "/api/coupons",
            "non token",
            ApiType.GET,
            null!
        ));
    }

    public async Task<Result> GetCouponById(int id, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.CouponAPI + $"/api/coupons/{id}",
            "non token",
            ApiType.GET,
            null!
        ));
    }

    
}
