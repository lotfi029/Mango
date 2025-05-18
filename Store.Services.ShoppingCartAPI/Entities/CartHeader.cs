using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Services.ShoppingCartAPI.Entities;

public class CartHeader
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string? UserId { get; set; }
    public string? CouponCode { get; set; }
    [NotMapped]
    public double DiscountTotal { get; set; }
    [NotMapped]
    public double CartTotal { get; set; }
}
