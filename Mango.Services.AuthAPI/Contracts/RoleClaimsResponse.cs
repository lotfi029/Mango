namespace Mango.Services.AuthAPI.Contracts;

public record RoleClaimsResponse(
    string Id,
    string Name,
    bool IsDeleted,
    IEnumerable<string> Permissions
);
