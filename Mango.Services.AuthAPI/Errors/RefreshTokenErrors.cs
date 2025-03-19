using Mango.Services.AuthAPI.Abstracts;

namespace Mango.Services.AuthAPI.Errors;

public static class RefreshTokenErrors
{
    public static readonly Error InvalidToken =
        Error.Unauthorized("Token.InvalidToken", "The token is expired or invalid.");

    public static readonly Error InvalidUserId =
        Error.NotFound("Token.InvalidUserId", "No user exists with this id.");

    public static readonly Error NoRefreshToken =
        Error.Unauthorized("Token.NoRefreshToken", "No refresh token provided.");
}