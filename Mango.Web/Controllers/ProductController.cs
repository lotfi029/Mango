using Mango.Web.Contracts.Products;
using Mango.Web.Services.IServices;
using System.Net;

namespace Mango.Web.Controllers;

[Route("product")]
public class ProductController(IProductService _productService) : Controller
{
    [HttpGet("")]
    public async Task<IActionResult> Index(CancellationToken ct = default)
    {
        var response = await _productService.GetAllProductsAsync(ct);

        if (!response.IsSucceed)
        {
            if (response.Error.StatusCode == HttpStatusCode.Forbidden || response.Error.StatusCode == HttpStatusCode.Unauthorized)
            {
                return RedirectToAction("forbidden", "Auth");
            }
        }

        return View(response.Value);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Details(int id, CancellationToken ct = default)
    {
        var result = await _productService.GetProductByIdAsync(id, ct);

        if (!result.IsSucceed)
            return NotFound();

        return View(result.Value);
    }

    [HttpGet("create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductRequest request, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var response = await _productService.CreateProductAsync(request, ct);
        if (!response.IsSucceed)
        {
            TempData["Error"] = response.Error.Description ?? "Failed to create product.";
            return View(request);
        }

        TempData["Success"] = "Product created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("edit/{id:int}")]
    public async Task<IActionResult> Edit(int id, CancellationToken ct = default)
    {
        var product = await _productService.GetProductByIdAsync(id, ct);

        if (!product.IsSucceed)
        {
            return NotFound();
        }

        var response = new ProductRequest (
            product.Value.Name,
            product.Value.Description,
            product.Value.Price,
            product.Value.CategoryName,
            product.Value.ImageUrl
            );

        return View(response);
    }

    [HttpPost("edit/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductRequest request, CancellationToken ct = default)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var result = await _productService.UpdateProductAsync(id, request, ct);

        if (!result.IsSucceed)
            return View(request);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost("delete/{id:int}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id, CancellationToken ct = default)
    {
        var response = await _productService.DeleteProductAsync(id, ct);
        if (!response.IsSucceed)
        {
            var productResponse = await _productService.GetProductByIdAsync(id, ct);
            if (!productResponse.IsSucceed)
            {
                return NotFound();
            }
            return View("Delete", productResponse.Value);
        }
        return RedirectToAction(nameof(Index));
    }
}