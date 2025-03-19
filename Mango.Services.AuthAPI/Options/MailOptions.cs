using System.ComponentModel.DataAnnotations;

namespace Mango.Services.AuthAPI.Options;
public class MailOptions
{
    public const string SectionName = "MailSettings";

    [Required(ErrorMessage = "Mail is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    public string Mail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Host is required.")]
    public string Host { get; set; } = string.Empty;

    [Required(ErrorMessage = "Display Name is required.")]
    public string DisplayName { get; set; } = string.Empty;

    [Range(1, 65535, ErrorMessage = "Port must be between 1 and 65535.")]
    public int Port { get; set; }

    public override string ToString()
        => $"{Mail}, {Password}, {Host}, {DisplayName}, {Port}";
}
