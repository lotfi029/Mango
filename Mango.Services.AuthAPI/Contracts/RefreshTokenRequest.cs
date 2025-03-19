namespace Mango.Services.AuthAPI.Contracts;

public record RefreshTokenRequest(
    string Token,
    string RefreshToken
);
