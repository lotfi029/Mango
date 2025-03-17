using Mango.Web.Abstracts;
using Mango.Web.Contracts;

namespace Mango.Web.Service.IService;

public interface ICouponService
{
    Task<Result> CreateCoupon(CouponRequest request, CancellationToken ct = default);
    Task<Result> GetCouponById(int id, CancellationToken ct = default);
    Task<Result> GetAllCoupons(CancellationToken ct = default);
    Task<Result> UpdateCoupon(int id, CouponRequest request, CancellationToken ct = default);
    Task<Result> DeleteCoupon(int id, CancellationToken ct = default);
}
