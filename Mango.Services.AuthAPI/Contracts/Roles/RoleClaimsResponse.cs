namespace Store.Services.AuthAPI.Contracts.Roles;

public record RoleClaimsResponse(
    string Id,
    string Name,
    bool IsDeleted,
    IEnumerable<string> Permissions
);
