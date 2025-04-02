using Mango.Services.AuthAPI.Abstracts;

namespace Mango.Services.AuthAPI.Errors;

public class RoleErrors
{
    public static readonly Error RoleNotFound =
        Error.NotFound("Role.NotFound", "Role not found.");

    public static readonly Error DuplicatedRole =
        Error.Conflict("Role.DuplicatedRole", "This role already exists.");

    public static readonly Error InvalidPermission =
        Error.BadRequest("Role.InvalidPermission", "The specified permission does not exist.");

}