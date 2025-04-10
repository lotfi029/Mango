using Mango.Services.ProductAPI.Abstracts;
using Mango.Services.ProductAPI.Contracts;
using Mango.Services.ProductAPI.Entities;
using Mango.Services.ProductAPI.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Services;

public class ProductService(AppDbContext dbContext) : IProductService
{
    public async Task<Result<ProductResponse>> GetByIdAsync(int id, CancellationToken ct)
    {
        var product = await dbContext.Products.FindAsync(new object[] { id }, ct);
        if (product is null)
            return Result.Fail<ProductResponse>(Error.NotFound("Product.NotFound","Product not found."));

        return Result.Success(product.Adapt<ProductResponse>());
    }

    public async Task<IEnumerable<ProductResponse>> GetAllAsync(CancellationToken ct)
    {
        var products = await dbContext.Products.ToListAsync(ct);
        return products.Adapt<IEnumerable<ProductResponse>>();
    }

    public async Task<Result<int>> AddAsync(ProductRequest request, CancellationToken ct)
    {
        try
        {
            var product = request.Adapt<Product>();
            await dbContext.Products.AddAsync(product, ct);
            await dbContext.SaveChangesAsync(ct);
            return Result.Success(product.Id);
        }
        catch (Exception ex)
        {
            return Result.Fail<int>(Error.BadRequest("Product.Exception",$"an exception accur {ex}"));
        }
    }

    public async Task<Result> UpdateAsync(int id, ProductRequest productRequest, CancellationToken ct)
    {
        var product = await dbContext.Products.FindAsync([id], ct);
        if (product is null)
            return Result.Fail(Error.NotFound("Product.NotFound","Product not found."));

        product.Name = productRequest.Name;
        product.Description = productRequest.Description;
        product.Price = productRequest.Price;
        product.CategoryName = productRequest.CategoryName;
        product.ImageUrl = productRequest.ImageUrl;

        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(ct);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(int id, CancellationToken ct)
    {
        var product = await dbContext.Products.FindAsync(new object[] { id }, ct);
        if (product is null)
            return Result.Fail(Error.NotFound("Product.NotFound", "Product not found."));

        dbContext.Products.Remove(product);
        await dbContext.SaveChangesAsync(ct);

        return Result.Success();
    }
}
