using Mango.Services.CouponAPI.Contracts;
using Mango.Services.CouponAPI.Entities;
using Mango.Services.CouponAPI.Repositories;
using Mapster;

namespace Mango.Services.CouponAPI.Services;

public class CouponService(IUnitOfWork unitOfWork) : ICouponService
{
    public async Task<IEnumerable<CouponResponse>> GetByIdAsync(int id, CancellationToken ct)
    {
        var coupon = await unitOfWork.Coupon.FindByIdAsync(ct, id);

        if (coupon is null) return Enumerable.Empty<CouponResponse>();

        List<CouponResponse> response = [];
        response.Add(coupon.Adapt<CouponResponse>());

        return response;
    }

    public async Task<IEnumerable<CouponResponse>> GetAllAsync(CancellationToken ct)
    {
        var coupons = await unitOfWork.Coupon.GetAllAsync(null!, ct: ct);

        return coupons.Adapt<IEnumerable<CouponResponse>>();
    }

    public async Task<int> AddAsync(CouponRequest request, CancellationToken ct)
    {
        try
        {
            var coupon = request.Adapt<Coupon>();

            await unitOfWork.Coupon.AddAsync(coupon, ct);
            await unitOfWork.CommitChangesAsync(ct);

            return coupon.Id;
        }
        catch
        {
            return -1;
        }
    }

    public async Task<bool> UpdateAsync(int id, CouponRequest coupon, CancellationToken ct)
    {
        var existingCoupon = await unitOfWork.Coupon.FindByIdAsync(ct, id);
        if (existingCoupon is null) return false;

        existingCoupon.Code = coupon.Code;
        existingCoupon.DiscountAmount = coupon.DiscountAmount;
        existingCoupon.MinAmount = coupon.MinAmount;

        unitOfWork.Coupon.Update(existingCoupon);
        await unitOfWork.CommitChangesAsync(ct);
        return true;
    }

    public async Task<bool> DeleteAsync(int id, CancellationToken ct)
    {

        await unitOfWork.Coupon.DeleteByIdAsync(ct, id);
        await unitOfWork.CommitChangesAsync(ct);
        
        return true;
    }
}
