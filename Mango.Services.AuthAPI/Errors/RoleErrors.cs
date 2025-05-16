using Store.Abstractions.Abstraction;

namespace Store.Services.AuthAPI.Errors;

public class RoleErrors
{
    public static readonly Error RoleNotFound =
        Error.NotFound("Role not found.");

    public static readonly Error DuplicatedRole =
        Error.Conflict("This role already exists.");

    public static readonly Error InvalidPermission =
        Error.BadRequest("The specified permission does not exist.");

}