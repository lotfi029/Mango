using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Entities;

public class AppRole : IdentityRole
{
    public bool IsDisable { get; set; }
}
