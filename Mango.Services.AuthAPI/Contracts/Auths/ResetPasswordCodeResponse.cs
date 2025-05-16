namespace Store.Services.AuthAPI.Contracts.Auths;

public record ResetPasswordCodeResponse (
    string Email,
    string Code
    );