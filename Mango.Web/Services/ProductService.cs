using Mango.Web.Abstracts;
using Mango.Web.Contracts.Products;
using Mango.Web.Services.IServices;
using Microsoft.Extensions.Options;

namespace Mango.Web.Services;

public class ProductService(
    IBaseService baseService,
    ITokenProvider tokenProvider,
    IOptions<ApiSettings> options) : IProductService
{
    private readonly ApiSettings _options = options.Value;
    private readonly string _route = "/api/products";
    private readonly string _token = tokenProvider.GetToken() ?? string.Empty;

    public async Task<Result> CreateProductAsync(ProductRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request(
            _options.ProductAPI + _route,
            _token,
            ApiType.POST,
            request
        ), ct);
    }

    public async Task<Result> UpdateProductAsync(int id, ProductRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request(
            _options.ProductAPI + $"{_route}/{id}",
            _token,
            ApiType.PUT,
            request
        ), ct);
    }

    public async Task<Result> DeleteProductAsync(int id, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request(
            _options.ProductAPI + $"{_route}/{id}",
            _token,
            ApiType.DELETE,
            null!
        ), ct);
    }

    public async Task<Result<IEnumerable<ProductResponse>>> GetAllProductsAsync(CancellationToken ct = default)
    {
        return await baseService.SendAsync<IEnumerable<ProductResponse>>(new Request(
            _options.ProductAPI + _route,
            _token,
            ApiType.GET,
            null!
        ), ct);
    }

    public async Task<Result<ProductResponse>> GetProductByIdAsync(int id, CancellationToken ct = default)
    {
        var response = await baseService.SendAsync<ProductResponse>(new Request(
            _options.ProductAPI + $"{_route}/{id}",
            _token,
            ApiType.GET,
            null!
        ), ct);

        if (!response.IsSucceed)
        {
            return Result.Fail<ProductResponse>(response.Error);
        }

        return Result.Success(response.Value);
    }
}