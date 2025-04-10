namespace Mango.Services.ProductAPI.Contracts;

public record ProductRequest(
    string Name,
    string Description,
    decimal Price,
    string CategoryName,
    string ImageUrl
    );