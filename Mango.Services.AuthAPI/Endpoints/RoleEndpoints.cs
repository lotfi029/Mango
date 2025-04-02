using Carter;
using Carter.ModelBinding;
using FluentValidation;
using FluentValidation.Results;
using Mango.Services.AuthAPI.Abstracts;
using Mango.Services.AuthAPI.Contracts;
using Mango.Services.AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.AuthAPI.Endpoints;

public class RoleEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/roles");

        group.MapGet("/", GetAllRoles)
            .WithName(nameof(GetAllRoles))
            .Produces<IEnumerable<RoleResponse>>(StatusCodes.Status200OK)
            .ProducesValidationProblem();

        group.MapGet("/{id}", GetRole)
            .WithName(nameof(GetRole))
            .Produces<RoleClaimsResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();

        group.MapPost("/", AddRole)
            .WithName(nameof(AddRole))
            .Produces<RoleClaimsResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status409Conflict)
            .ProducesValidationProblem();

        group.MapPut("/{id}", UpdateRole)
            .WithName(nameof(UpdateRole))
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status409Conflict)
            .ProducesValidationProblem();

        group.MapPatch("/{id}/toggle", ToggleRole)
            .WithName(nameof(ToggleRole))
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();
    }

    private async Task<IResult> GetAllRoles(
        HttpRequest req,
        [FromQuery] bool? includeDisable,
        [FromServices] IRoleService roleService,
        CancellationToken ct)
    {
        var roles = await roleService.GetAllAsync(includeDisable, ct);
        return TypedResults.Ok(roles);
    }

    private async Task<IResult> GetRole(
        string id,
        [FromServices] IRoleService roleService)
    {
        var result = await roleService.GetAsync(id);
        return result.IsSucceed
            ? TypedResults.Ok(result.Value)
            : result.ToProblem();
    }

    private async Task<IResult> AddRole(
        HttpContext http,
        [FromBody] RoleRequest request,
        [FromServices] IRoleService roleService,
        IValidator<RoleRequest> validator,
        CancellationToken ct)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        

        var result = await roleService.AddAsync(request);
        return result.IsSucceed
            ? TypedResults.Ok(result.Value)
            : result.ToProblem();
    }

    private async Task<IResult> UpdateRole(
        string id,
        [FromBody] RoleRequest request,
        [FromServices] IRoleService roleService,
        IValidator<RoleRequest> validator,
        CancellationToken ct)
    {
        ValidationResult validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        

        var result = await roleService.UpdateAsync(id, request, ct);
        return result.IsSucceed
            ? TypedResults.Ok()
            : result.ToProblem();
    }

    private async Task<IResult> ToggleRole(
        string id,
        [FromServices] IRoleService roleService)
    {
        var result = await roleService.ToggleAsync(id);
        return result.IsSucceed
            ? TypedResults.Ok()
            : result.ToProblem();
    }
}
