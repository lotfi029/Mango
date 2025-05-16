namespace Store.Services.AuthAPI.Contracts.Roles;

public record RoleRequest(
    string Name,
    IList<string> Permission
);
