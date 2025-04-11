using Mango.Web.Abstracts;
using Mango.Web.Contracts;

namespace Mango.Web.Services.IServices;

public interface ICouponService
{
    Task<Result> CreateCouponAsync(CouponRequest request, CancellationToken ct = default);
    Task<Result> UpdateCouponAsync(int id, CouponRequest request, CancellationToken ct = default);
    Task<Result> DeleteCouponAsync(int id, CancellationToken ct = default);
    Task<Result<CouponResponse>> GetCouponByIdAsync(int id, CancellationToken ct = default);
    Task<Result<IEnumerable<CouponResponse>>> GetAllCouponsAsync(CancellationToken ct = default);
}
