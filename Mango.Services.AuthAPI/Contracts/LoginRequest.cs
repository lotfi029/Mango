namespace Mango.Services.AuthAPI.Contracts;

public record LoginRequest(
    string Email,
    string Password
);
