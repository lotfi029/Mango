namespace Mango.Services.AuthAPI.Contracts;

public record ResetPasswordCodeResponse (
    string Email,
    string Code
    );