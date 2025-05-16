using Store.Services.AuthAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace Store.Services.AuthAPI.Authentication;

public interface IJwtProvider
{
    (string token, int expiresIn) GenerateToken(AppUser user, IEnumerable<string> roles, IEnumerable<string> permissions);
    string? ValidateToken(string token);
}
