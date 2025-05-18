 namespace Store.Services.ShoppingCartAPI.DTOs;

public class CartDto
{
    public CartHeaderDto CartHeaderDto { get; set; } = default!;
    public List<CartDetailsDto>? CartDetails { get; set; }
}
