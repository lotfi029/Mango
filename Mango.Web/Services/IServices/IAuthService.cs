using Mango.Web.Abstracts;

namespace Mango.Web.Services.IServices;

public interface IAuthService
{
    Task<Result<AuthResponse>> LoginAsync(LoginRequest request, CancellationToken ct = default);
    Task<Result<AuthResponse>> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken ct = default);
    Task<Result> RevokeTokenAsync(RefreshTokenRequest request, CancellationToken ct = default);
    Task<Result<ConfirmEmailRequest>> RegisterAsync(RegisterRequest request, CancellationToken ct = default);
    Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request, CancellationToken ct = default);
    Task<Result<ConfirmEmailRequest>> ResendEmailConfirmationAsync(ResendConfirmationEmailRequest request, CancellationToken ct = default);
    Task<Result<ResetPasswordResponse>> SendResetPasswordCodeAsync(ForgotPasswordRequest request, CancellationToken ct = default);
    Task<Result> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken ct = default);
}