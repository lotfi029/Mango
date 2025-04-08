using Mango.Services.AuthAPI.Abstracts;
using Mango.Services.AuthAPI.Contracts;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace Mango.Services.AuthAPI.Services;

public interface IAuthService
{
    Task<Result<AccessTokenResponse>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken = default);
    Task<Result<AccessTokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default);
    Task<Result> RevokeRefreshTokenAsync(RefreshTokenRequest reqeust, CancellationToken cancellationToken = default);
    Task<Result<ConfirmEmailRequest>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request);
    Task<Result<ConfirmEmailRequest>> ReConfirmAsync(ResendConfirmationEmailRequest request);
    Task<Result> SendResetPasswordCodeAsync(ForgotPasswordRequest request);
    Task<Result> ResetPasswordAsync(ResetPasswordRequest request);
}
