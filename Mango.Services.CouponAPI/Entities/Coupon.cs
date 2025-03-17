namespace Mango.Services.CouponAPI.Entities;

public class Coupon
{
    public int Id { get; set; }
    public float DiscountAmount { get; set; }
    public int MinAmount { get; set; }
    public string Code { get; set; } = string.Empty;
}