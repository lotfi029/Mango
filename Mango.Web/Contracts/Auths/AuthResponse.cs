namespace Mango.Web.Contracts.Auths;

public record AuthResponse (string AccessToken, long ExpiresIn, string RefreshToken, string TokenType = "bearer");
