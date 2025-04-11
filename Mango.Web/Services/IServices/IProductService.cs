using Mango.Web.Abstracts;
using Mango.Web.Contracts.Products;

namespace Mango.Web.Services.IServices;

public interface IProductService
{
    Task<Result> CreateProductAsync(ProductRequest request, CancellationToken ct = default);
    Task<Result> UpdateProductAsync(int id, ProductRequest request, CancellationToken ct = default);
    Task<Result> DeleteProductAsync(int id, CancellationToken ctr = default);
    Task<Result<ProductResponse>> GetProductByIdAsync(int id, CancellationToken ct = default);
    Task<Result<IEnumerable<ProductResponse>>> GetAllProductsAsync(CancellationToken ct = default);
}