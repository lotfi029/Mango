namespace Mango.Services.AuthAPI.Contracts;

public record ResetPasswordRequest(
    string Email,
    string ResetCode,
    string NewPassword
);
