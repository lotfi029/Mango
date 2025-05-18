namespace Store.Services.ShoppingCartAPI.DTOs;

public class CartDetailsDto
{
    public Guid Id { get; set; }
    public Guid CartHeaderId { get; set; }
    public CartHeaderDto CartHeader { get; set; } = default!;
    public Guid ProductId { get; set; }
    public ProductDto Product { get; set; } = default!;
    public int Count { get; set; }
}
