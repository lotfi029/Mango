using Store.Abstractions.Abstraction;

namespace Store.Services.AuthAPI.Errors;

public class UserErrors
{
    public static Error UserNotFound => 
        Error.NotFound("User not found.");
}
