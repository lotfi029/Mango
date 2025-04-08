namespace Mango.Web.Contracts.Auths;

public record RefreshTokenRequest(
    string Token,
    string RefreshToken
);
