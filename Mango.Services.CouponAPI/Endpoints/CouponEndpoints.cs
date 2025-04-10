using Carter;
using Microsoft.AspNetCore.Mvc;
using Mango.Services.CouponAPI.Services;
using Mango.Services.CouponAPI.Contracts;
using Mango.Services.CouponAPI.Abstracts.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Mango.Services.CouponAPI.Endpoints;

public class CouponEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/coupons")
            .RequireAuthorization();

        group.MapGet("/{id:int}", Get)
            .WithName(nameof(Get))
            .Produces<CouponResponse>(200)
            .ProducesProblem(404)
            .RequireAuthorization(DefaultRoles.User);

        group.MapGet("/", GetAll)
            .Produces<CouponResponse>(200)
            .RequireAuthorization(DefaultRoles.User);

        group.MapPost("/", Create)
            .ProducesProblem(400)
            .RequireAuthorization(DefaultRoles.Admin);

        group.MapPut("/{id:int}", Update)
            .ProducesProblem(400)
            .RequireAuthorization(DefaultRoles.Admin);

        group.MapDelete("/{id:int}", Delete)
            .ProducesProblem(400)
            .RequireAuthorization(DefaultRoles.Admin);
    }

    private async Task<IResult> Get(
        int id,
        ICouponService _couponService,
        CancellationToken ct)
    {
        var response = await _couponService.GetByIdAsync(id, ct);
        return response is null ? TypedResults.NotFound() : TypedResults.Ok(response);
    }

    private async Task<IResult> GetAll(
        ICouponService _couponService,
        CancellationToken ct)
    {
        var response = await _couponService.GetAllAsync(ct);
        return TypedResults.Ok(response);
    }

    private async Task<IResult> Create(
        [FromBody] CouponRequest request,
        HttpContext context,
        HttpRequest httpRequest,
        HttpResponse response,
        ICouponService _couponService,
        CancellationToken ct)
    {
        var id = await _couponService.AddAsync(request, ct);

        if (id == -1) 
            return TypedResults.BadRequest();

        return TypedResults.CreatedAtRoute(nameof(Get), new { id });
    }

    private async Task<IResult> Update(
        int id,
        [FromBody] CouponRequest request,
        ICouponService _couponService,
        CancellationToken ct)
    {
        var result = await _couponService.UpdateAsync(id, request, ct);
        
        return result
            ? TypedResults.NoContent() 
            : TypedResults.BadRequest();
    }

    private async Task<IResult> Delete(
        int id,
        ICouponService _couponService,
        CancellationToken ct)
    {

        var result = await _couponService.DeleteAsync(id, ct);

        return result 
            ? TypedResults.NoContent()
            : TypedResults.BadRequest();
    }
}
