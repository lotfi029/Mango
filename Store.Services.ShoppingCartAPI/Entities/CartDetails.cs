using Store.Services.ShoppingCartAPI.DTOs;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Services.ShoppingCartAPI.Entities;

public class CartDetails
{
    public Guid Id { get; set; } = Guid.CreateVersion7();
    public Guid CartHeaderId { get; set; }
    public CartHeader CartHeader { get; set; } = default!;
    public Guid ProductId { get; set; }
    [NotMapped]
    public ProductDto Product { get; set; } = default!;
    public int Count { get; set; } = 1;
}
