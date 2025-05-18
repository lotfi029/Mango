namespace Store.Services.ShoppingCartAPI.DTOs;

public class ProductDto
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public string Name { get; set; } = default!;
    public string Price { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string CategoryName { get; set; } = default!;
    public string ImageUrl { get; set; } = default!;
}
