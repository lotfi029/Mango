namespace Store.Services.AuthAPI.Contracts.Roles;
public record RoleResponse(
    string Id,
    string Name,
    bool IsDisable
);
