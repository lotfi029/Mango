using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Mango.Web.Abstracts;

public class ApiSettings
{
    public const string Section = "ApiSettings";
    public const string TokenCookie = "Jwt-Access-Token";
    [Required]
    [Length(0, 100)]
    public string CouponAPI { get; set; } = string.Empty;
    [Required]
    [Length(0, 100)]
    public string AuthAPI { get; set; } = string.Empty;
    [Required]
    [Length(0, 100)]
    public string ProductAPI { get; set; } = string.Empty;
}
