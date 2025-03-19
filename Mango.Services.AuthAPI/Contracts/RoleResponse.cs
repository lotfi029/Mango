namespace Mango.Services.AuthAPI.Contracts;
public record RoleResponse(
    string Id,
    string Name,
    bool IsDisable
);
