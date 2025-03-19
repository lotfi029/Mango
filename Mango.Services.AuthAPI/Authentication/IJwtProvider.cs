using Mango.Services.AuthAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.AuthAPI.Authentication;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(AppUser user, IEnumerable<string> roles, IEnumerable<string> permissions);
    string? ValidateToken(string token);
}
