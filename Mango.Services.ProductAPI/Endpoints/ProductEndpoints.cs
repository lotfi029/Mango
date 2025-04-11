using Carter;
using Carter.ModelBinding;
using FluentValidation;
using Mango.Services.ProductAPI.Abstracts;
using Mango.Services.ProductAPI.Abstracts.Constants;
using Mango.Services.ProductAPI.Contracts;
using Mango.Services.ProductAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Endpoints;

public class ProductEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/products")
            .RequireAuthorization();

        group.MapGet("/{id:int}", GetById)
            .WithName(nameof(GetById))
            .Produces<ProductResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();

        group.MapGet("/", GetAll)
            .WithName(nameof(GetAll))
            .Produces<IEnumerable<ProductResponse>>(StatusCodes.Status200OK);

        group.MapPost("/", Add)
            .WithName(nameof(Add))
            .Produces<int>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem()
            .RequireAuthorization(DefaultRoles.Admin);

        group.MapPut("/{id:int}", Update)
            .WithName(nameof(Update))
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .ProducesValidationProblem()
            .RequireAuthorization(DefaultRoles.Admin);

        group.MapDelete("/{id:int}", Delete)
            .WithName(nameof(Delete))
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .RequireAuthorization(DefaultRoles.Admin);
    }

    private async Task<IResult> GetById(
        int id,
        [FromServices] IProductService productService,
        CancellationToken ct)
    {
        var result = await productService.GetByIdAsync(id, ct);
        return result.IsSucceed
            ? TypedResults.Ok(result.Value)
            : result.ToProblem();
    }

    private async Task<IResult> GetAll(
        [FromServices] IProductService productService,
        CancellationToken ct)
    {
        var result = await productService.GetAllAsync(ct);
        return TypedResults.Ok(result);
    }

    private async Task<IResult> Add(
        [FromBody] ProductRequest request,
        [FromServices] IProductService productService,
        [FromServices] IValidator<ProductRequest> validator,
        CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        }

        var result = await productService.AddAsync(request, ct);
        return result.IsSucceed
            ? TypedResults.Created($"/products/{result.Value}", result.Value)
            : result.ToProblem();
    }

    private async Task<IResult> Update(
        int id,
        [FromBody] ProductRequest request,
        [FromServices] IProductService productService,
        [FromServices] IValidator<ProductRequest> validator,
        CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        }

        var result = await productService.UpdateAsync(id, request, ct);
        return result.IsSucceed
            ? TypedResults.NoContent()
            : result.ToProblem();
    }

    private async Task<IResult> Delete(
        int id,
        [FromServices] IProductService productService,
        CancellationToken ct)
    {
        var result = await productService.DeleteAsync(id, ct);
        return result.IsSucceed
            ? TypedResults.NoContent()
            : result.ToProblem();
    }
}