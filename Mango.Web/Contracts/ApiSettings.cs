using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Mango.Web.Contracts;

public class ApiSettings
{
    public const string Section = "ApiSettings";
    [Required]
    [Length(0, 100)]
    public string CouponAPI { get; set; } = string.Empty;
    [Required]
    [Length(0, 100)]
    public string AuthAPI { get; set; } = string.Empty;
}
