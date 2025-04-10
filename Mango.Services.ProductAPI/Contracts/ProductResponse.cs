namespace Mango.Services.ProductAPI.Contracts;

public record ProductResponse(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string CategoryName,
    string ImageUrl
    );
