namespace Store.Services.AuthAPI.Contracts.Auths;

public record LoginRequest(
    string Email,
    string Password
);
