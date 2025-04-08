using Mango.Services.CouponAPI.Contracts;

namespace Mango.Services.CouponAPI.Services;

public interface ICouponService
{
    Task<IEnumerable<CouponResponse>> GetByIdAsync(int id, CancellationToken ct);
    Task<IEnumerable<CouponResponse>> GetAllAsync(CancellationToken ct);
    Task<int> AddAsync(CouponRequest coupon, CancellationToken ct);
    Task<bool> UpdateAsync(int id, CouponRequest coupon, CancellationToken ct);
    Task<bool> DeleteAsync(int id, CancellationToken ct);
}
