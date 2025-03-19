using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsDisable { get; set; } = false;

    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}
