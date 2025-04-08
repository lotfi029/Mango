using Mango.Web.Abstracts;
using Mango.Web.Contracts;
using Mango.Web.Contracts.Auths;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.Extensions.Options;

namespace Mango.Web.Service;

public class AuthService(
    IBaseService baseService,
    IOptions<ApiSettings> options) : IAuthService
{
    private readonly ApiSettings _options = options.Value;
    private readonly string _route = "/api/auth";

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync<AuthResponse>(new Request
        (
            _options.AuthAPI + $"{_route}/login",
            "non token",
            ApiType.POST,
            request
        ));
    }

    public async Task<Result<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync<AuthResponse>(new Request
        (
            _options.AuthAPI + $"{_route}/refresh",
            "non token",
            ApiType.POST,
            request
        ));
    }

    public async Task<Result> RevokeTokenAsync(RefreshTokenRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.AuthAPI + $"{_route}/revoke",
            "non token",
            ApiType.POST,
            request
        ));
    }

    public async Task<Result<ConfirmEmailRequest>> RegisterAsync(RegisterRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync<ConfirmEmailRequest>(new Request
        (
            _options.AuthAPI + $"{_route}/register",
            "non token",
            ApiType.POST,
            request
        ));
    }

    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.AuthAPI + $"{_route}/confirm",
            "non token",
            ApiType.POST,
            request
        ));
    }

    public async Task<Result<ConfirmEmailRequest>> ResendEmailConfirmationAsync(ResendConfirmationEmailRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync<ConfirmEmailRequest>(new Request
        (
            _options.AuthAPI + $"{_route}/reconfirm",
            "non token",
            ApiType.POST,
            request
        ));
    }

    public async Task<Result> SendResetPasswordCodeAsync(ForgotPasswordRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.AuthAPI + $"{_route}/forgot-password",
            "non token",
            ApiType.POST,
            request
        ));
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync(new Request
        (
            _options.AuthAPI + $"{_route}/reset-password",
            "non token",
            ApiType.POST,
            request
        ));
    }
}