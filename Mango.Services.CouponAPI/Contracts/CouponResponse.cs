namespace Mango.Services.CouponAPI.Contracts;

public record CouponResponse(
    int Id,
    float DiscountAmount,
    int MinAmount,
    string Code
    );