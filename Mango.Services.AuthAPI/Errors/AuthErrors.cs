using Store.Abstractions.Abstraction;

namespace Store.Services.AuthAPI.Errors;


public record AuthErrors
{
    public static readonly Error InvalidCredentials =
        Error.Unauthorized("Invalid email/password");

    public static readonly Error DisabledUser =
        Error.Unauthorized("Disabled user. Please contact your administrator.");

    public static readonly Error LockedUser =
        Error.Locked("Locked user. Please contact your administrator.");

    public static readonly Error NotFound =
        Error.NotFound("User not found.");

    public static readonly Error DuplicatedEmail =
        Error.Conflict("Another user with the same email already exists.");

    public static readonly Error EmailIsNotConfirmed =
        Error.Unauthorized("Email is not confirmed.");

    public static readonly Error InvalidCode =
        Error.BadRequest("Invalid code.");

    public static readonly Error DuplicatedConfirmation =
        Error.BadRequest("Duplicated confirmation.");
}
