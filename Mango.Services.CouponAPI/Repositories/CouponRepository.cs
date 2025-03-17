using Mango.Services.CouponAPI.Entities;
using Mango.Services.CouponAPI.Persistence;

namespace Mango.Services.CouponAPI.Repositories;

public class CouponRepository(AppDbContext context) : Repository<Coupon>(context), ICouponRepository
{
}
