 namespace Store.Services.ShoppingCartAPI.DTOs;

public record CouponDto(
    Guid Id,
    string CouponCode,
    double DiscountAmount,
    int MinAmount
);
