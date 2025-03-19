using Mango.Services.AuthAPI.Abstracts;

namespace Mango.Services.AuthAPI.Errors;


public record UserErrors
{
    public static readonly Error InvalidCredentials =
        Error.Unauthorized("User.InvalidCredentials", "Invalid email/password");

    public static readonly Error DisabledUser =
        Error.Forbidden("User.DisabledUser", "Disabled user. Please contact your administrator.");

    public static readonly Error LockedUser =
        Error.Locked("User.LockedUser", "Locked user. Please contact your administrator.");

    public static readonly Error NotFound =
        Error.NotFound("User.NotFound", "User not found.");

    public static readonly Error DuplicatedEmail =
        Error.Conflict("User.DuplicatedEmail", "Another user with the same email already exists.");

    public static readonly Error EmailIsNotConfirmed =
        Error.Forbidden("User.EmailIsNotConfirmed", "Email is not confirmed.");

    public static readonly Error InvalidCode =
        Error.BadRequest("User.InvalidCode", "Invalid code.");

    public static readonly Error DuplicatedConfirmation =
        Error.BadRequest("User.DuplicatedConfirmation", "Duplicated confirmation.");
}
