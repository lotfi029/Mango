using Mango.Services.ProductAPI.Abstracts;
using Mango.Services.ProductAPI.Contracts;

namespace Mango.Services.ProductAPI.Services;

public interface IProductService
{
    Task<Result<ProductResponse>> GetByIdAsync(int id, CancellationToken ct);
    Task<IEnumerable<ProductResponse>> GetAllAsync(CancellationToken ct);
    Task<Result<int>> AddAsync(ProductRequest request, CancellationToken ct);
    Task<Result> UpdateAsync(int id, ProductRequest product, CancellationToken ct);
    Task<Result> DeleteAsync(int id, CancellationToken ct);
}
