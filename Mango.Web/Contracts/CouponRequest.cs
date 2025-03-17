namespace Mango.Web.Contracts;

public record CouponRequest(
    float DiscountAmount,
    int MinAmount,
    string Code
    );
