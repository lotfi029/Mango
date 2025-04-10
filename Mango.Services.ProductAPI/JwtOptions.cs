using System.ComponentModel.DataAnnotations;

namespace Mango.Services.ProductAPI;

public class JwtOptions
{
    public const string SectionName = "JwtOptions";
    [Required]
    public string Key { get; set; } = string.Empty;
    [Required]
    public string Issuer { get; set; } = string.Empty;
    [Required]
    public string Audience { get; set; } = string.Empty;
    [Required]
    [Range(1, int.MaxValue)]
    public int ExpiryMinutes { get; set; }
}
