namespace Mango.Web.Contracts.Products;

public record ProductRequest(
    string Name,
    string Description,
    decimal Price,
    string CategoryName,
    string ImageUrl
    );