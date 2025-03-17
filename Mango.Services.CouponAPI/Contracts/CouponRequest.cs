namespace Mango.Services.CouponAPI.Contracts;

public record CouponRequest(
    float DiscountAmount,
    int MinAmount,
    string Code
    );
