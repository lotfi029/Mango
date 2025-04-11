using Mango.Web.Abstracts;
using Mango.Web.Services.IServices;
using Microsoft.Extensions.Options;

namespace Mango.Web.Services;

public class AuthService(
    IBaseService baseService,
    IOptions<ApiSettings> options) : IAuthService
{
    private readonly ApiSettings _options = options.Value;
    private readonly string _route = "/auth";

    public async Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default)
    {
        var response = await baseService.SendAsync<AuthResponse>(new Request
        (
            _options.AuthAPI + $"{_route}/token",
            "non token",
            ApiType.POST,
            request
        ));

        return response;
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
        var response =  await baseService.SendAsync<ConfirmEmailRequest>(new Request
        (
            _options.AuthAPI + $"{_route}/register",
            "non token",
            ApiType.POST,
            request
        ));


        return response;
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

    public async Task<Result<ResetPasswordResponse>> SendResetPasswordCodeAsync(ForgotPasswordRequest request, CancellationToken ct = default)
    {
        return await baseService.SendAsync<ResetPasswordResponse>(new Request
        (
            _options.AuthAPI + $"{_route}/forgot-password",
            "non token",
            ApiType.POST,
            request
        ));
    }

    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken ct = default)
    {
        var requestApi = new ResetPasswordRequestApi(request.Email, request.ResetCode, request.Password);

        var response = await baseService.SendAsync(new Request
        (
            _options.AuthAPI + $"{_route}/reset-password",
            "non token",
            ApiType.POST,
            requestApi
        ));

        return response;
    }
}