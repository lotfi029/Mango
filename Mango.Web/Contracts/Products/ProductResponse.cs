namespace Mango.Web.Contracts.Products;

public record ProductResponse(
    int Id,
    string Name,
    string Description,
    decimal Price,
    string CategoryName,
    string ImageUrl
    );
