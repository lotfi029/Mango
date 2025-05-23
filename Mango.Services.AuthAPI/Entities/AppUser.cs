﻿using Microsoft.AspNetCore.Identity;

namespace Store.Services.AuthAPI.Entities;

public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public bool IsDisable { get; set; } = false;
    public string? ImageUrl { get; set; } = null;
    public DateTime CreateAt { get; set; }
    public DateTime? LastLoginAt { get; set; } = null;
    public ICollection<RefreshToken> RefreshTokens { get; set; } = [];
}
