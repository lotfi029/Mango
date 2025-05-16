namespace Store.Services.AuthAPI.Contracts.Auths;

public record ResetPasswordRequest(
    string Email,
    string ResetCode,
    string NewPassword
);
