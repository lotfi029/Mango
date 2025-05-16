using Carter;
using FluentValidation;
using Store.Services.AuthAPI.Contracts.Users;
using Store.Services.AuthAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Store.Services.AuthAPI.Abstracts;
using Store.Services.AuthAPI.Contracts.Files;

namespace Store.Services.AuthAPI.Endpoints;

public class UserEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("api/users")
            .RequireAuthorization();

        group.MapPut("", UpdateProfile)
            .WithName("UpdateProfile")
            .WithTags("User")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();

        group.MapGet("", GetProfile)
            .WithName("GetProfile")
            .WithTags("User")
            .Produces<UserResponse>(StatusCodes.Status200OK)
            .ProducesValidationProblem();

        group.MapPut("change-photo", ChnageProfilePhoto)
            .WithName("ChangeProfilePhoto")
            .WithTags("User")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();

        group.MapDelete("", DeleteAccount)
            .WithName("DeleteAccount")
            .WithTags("User")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesValidationProblem();
    }

    private async Task<IResult> UpdateProfile(
        [FromBody] UpdateUserRequest request,
        [FromServices] IUserService userRequest,
        [FromServices] IValidator<UpdateUserRequest> validator,
        HttpContext httpClient,
        CancellationToken ct
        )
    {
        var validationResult = await validator.ValidateAsync(request, ct);

        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }

        var userId = httpClient.User.FindFirstValue(ClaimTypes.NameIdentifier)! ?? string.Empty;

        var result = await userRequest.UpdateProfileAsync(userId,request, ct);

        return result.IsSuccess
            ? Results.NoContent()
            : result.ToProblem();
    }
    private async Task<IResult> GetProfile(
        [FromServices] IUserService userService,
        [FromServices] IValidator<UpdateUserRequest> validator,
        HttpContext httpContext,
        CancellationToken ct)
    {
        var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)! ?? string.Empty;
        var result = await userService.GetProfileAsync(userId, ct);
        return result.IsSuccess 
            ? Results.Ok(result.Data) 
            : result.ToProblem();
    }
    private async Task<IResult> ChnageProfilePhoto(
        [FromBody] ImageRequest request,
        [FromServices] IUserService userService,
        [FromServices] IValidator<ImageRequest> validator,
        [FromServices] HttpContext httpContext,
        [FromServices] CancellationToken ct)
    {

        var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)! ?? string.Empty;

        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return Results.ValidationProblem(validationResult.ToDictionary());
        }
        var result = await userService.UpdateProfileImageAsync(userId, request.Image, ct);
        
        return result.IsSuccess 
            ? Results.NoContent() : 
            result.ToProblem();
    }
    private async Task<IResult> DeleteAccount(
        [FromServices] IUserService userService,
        [FromServices] HttpContext httpContext,
        CancellationToken ct)
    {
        var userId = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)! ?? string.Empty;
        var result = await userService.DeleteAccountAsync(userId, ct);
        return result.IsSuccess
            ? Results.NoContent()
            : result.ToProblem();
    }
}
