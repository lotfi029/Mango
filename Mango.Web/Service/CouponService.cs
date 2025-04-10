using Mango.Web.Abstracts;
using Mango.Web.Contracts;
using Mango.Web.Service.IService;
using Microsoft.Extensions.Options;

namespace Mango.Web.Service;

public class CouponService(
    IBaseService baseService,
    ITokenProvider _tokenProvider,
    IOptions<ApiSettings> options) : ICouponService
{
    private readonly ApiSettings _options = options.Value;
    private readonly string _route = "/api/coupons";
    private readonly string _token = _tokenProvider.GetToken() ?? string.Empty;
    public async Task<Result> CreateCouponAsync(CouponRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.CouponAPI + _route,
            _token,
            ApiType.POST,
            request
        ));
    }
    public async Task<Result> UpdateCouponAsync(int id, CouponRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.CouponAPI + $"{_route}/{id}",
            _token,
            ApiType.PUT,
            request
        ));
    }
    public async Task<Result> DeleteCouponAsync(int id, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.CouponAPI + $"{_route}/{id}",
            _token,
            ApiType.DELETE,
            null!
        ));
    }

    public async Task<Result<IEnumerable<CouponResponse>>> GetAllCouponsAsync(CancellationToken ct = default)
    {
        return await baseService.SendAsync<IEnumerable<CouponResponse>>(new Request
        (
            _options.CouponAPI + $"{_route}",
            _token,
            ApiType.GET,
            null!
        ));
    }

    public async Task<Result<CouponResponse>> GetCouponByIdAsync(int id, CancellationToken ct = default)
    {
        var response = await baseService.SendAsync<CouponResponse>(new Request
        (
            _options.CouponAPI + $"{_route}/{id}",
            _token,
            ApiType.GET,
            null!
        ));

        if (!response.IsSucceed)
        {
            return Result.Fail<CouponResponse>(response.Error);
        }

        return Result.Success(response.Value);
    }

    
}
