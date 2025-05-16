using Store.Abstractions.Abstraction;

namespace Store.Services.AuthAPI.Errors;

public static class RefreshTokenErrors
{
    public static readonly Error InvalidToken =
        Error.Unauthorized("The token is expired or invalid.");

    public static readonly Error InvalidUserId =
        Error.NotFound("No user exists with this id.");

    public static readonly Error NoRefreshToken =
        Error.Unauthorized("No refresh token provided.");
}