namespace Mango.Services.AuthAPI.Contracts;

public record RoleRequest(
    string Name,
    IList<string> Permission
);
