namespace Store.Services.AuthAPI.Contracts.Auths;

public record RefreshTokenRequest(
    string Token,
    string RefreshToken
);
