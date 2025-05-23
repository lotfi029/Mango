﻿using Carter;
using Carter.ModelBinding;
using FluentValidation;
using FluentValidation.Results;
using Store.Services.AuthAPI.Abstracts;
using Store.Services.AuthAPI.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Mvc;
using Store.Services.AuthAPI.Contracts.Auths;

namespace Store.Services.AuthAPI.Endpoints;

public class AuthEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth");

        group.MapPost("/token", GetToken)
            .WithName(nameof(GetToken))
            .Produces<AccessTokenResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .Produces<ProblemDetails>(StatusCodes.Status423Locked)
            .ProducesValidationProblem();

        group.MapPost("/refresh", GetRefreshToken)
            .WithName(nameof(GetRefreshToken))
            .Produces<AccessTokenResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();

        group.MapPost("/revoke", RevokeRefreshToken)
            .WithName(nameof(RevokeRefreshToken))
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status401Unauthorized)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .ProducesValidationProblem();

        group.MapPost("/register", Register)
            .WithName(nameof(Register))
            .Produces<ConfirmEmailRequest>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status409Conflict)
            .ProducesValidationProblem();

        group.MapPost("/confirm", ConfirmEmail)
            .WithName(nameof(ConfirmEmail))
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();

        group.MapPost("/reconfirm", ReConfirm)
            .WithName(nameof(ReConfirm))
            .Produces<ConfirmEmailRequest>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();

        group.MapPost("/forgot-password", SendResetPasswordCode)
            .WithName(nameof(SendResetPasswordCode))
            .Produces<ResetPasswordCodeResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .ProducesValidationProblem();

        group.MapPost("/reset-password", ResetPassword)
            .WithName(nameof(ResetPassword))
            .Produces(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status409Conflict)
            .ProducesValidationProblem();
    }

    private async Task<IResult> GetToken(
        [FromBody] LoginRequest request,
        [FromServices] IAuthService authService,
        IValidator<LoginRequest> validator,
        CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            validationResult.ToDictionary();
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        }

        var result = await authService.GetTokenAsync(request, ct);

        return result.IsSuccess
            ? TypedResults.Ok(result.Data)
            : result.ToProblem();
    }

    private async Task<IResult> GetRefreshToken(
        [FromBody] RefreshTokenRequest request,
        [FromServices] IAuthService authService,
        IValidator<RefreshTokenRequest> validator,
        CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        }

        var result = await authService.GetRefreshTokenAsync(request, ct);
        return result.IsSuccess
            ? TypedResults.Ok(result.Data)
            : result.ToProblem();
    }

    private async Task<IResult> RevokeRefreshToken(
        [FromBody] RefreshTokenRequest request,
        [FromServices] IAuthService authService,
        IValidator<RefreshTokenRequest> validator,
        CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        }

        var result = await authService.RevokeRefreshTokenAsync(request, ct);
        return result.IsSuccess
            ? TypedResults.Ok()
            : result.ToProblem();
    }

    private async Task<IResult> Register(
        [FromBody] RegisterRequest request,
        [FromServices] IAuthService authService,
        IValidator<RegisterRequest> validator,
        CancellationToken ct)
    {
        var validationResult = await validator.ValidateAsync(request, ct);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        }

        var result = await authService.RegisterAsync(request, ct);
        return result.IsSuccess
            ? TypedResults.Ok(result.Data)
            : result.ToProblem();
    }

    private async Task<IResult> ConfirmEmail(
        [FromBody] ConfirmEmailRequest request,
        [FromServices] IAuthService authService,
        IValidator<ConfirmEmailRequest> validator)
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        }

        var result = await authService.ConfirmEmailAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok()
            : result.ToProblem();
    }

    private async Task<IResult> ReConfirm(
        [FromBody] ResendConfirmationEmailRequest request,
        [FromServices] IAuthService authService,
        IValidator<ResendConfirmationEmailRequest> validator
        )
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        }

        var result = await authService.ReConfirmAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result.Data)
            : result.ToProblem();
    }

    private async Task<IResult> SendResetPasswordCode(
        [FromBody] ForgotPasswordRequest request,
        [FromServices] IAuthService authService,
        IValidator<ForgotPasswordRequest> validator
        )
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        }

        var result = await authService.SendResetPasswordCodeAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok(result.Data)
            : result.ToProblem();
    }

    private async Task<IResult> ResetPassword(
        [FromBody] ResetPasswordRequest request,
        [FromServices] IAuthService authService,
        IValidator<ResetPasswordRequest> validator
        )
    {
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
        {
            return TypedResults.ValidationProblem(validationResult.GetValidationProblems());
        }

        var result = await authService.ResetPasswordAsync(request);
        return result.IsSuccess
            ? TypedResults.Ok()
            : result.ToProblem();
    }
}
