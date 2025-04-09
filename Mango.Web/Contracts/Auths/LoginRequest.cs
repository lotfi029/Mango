using System.ComponentModel.DataAnnotations;

namespace Mango.Web.Contracts.Auths;

public record LoginRequest(
    string Email,
    string Password
);

//public class LoginRequest(string email, string password)
//{
//    [EmailAddress]
//    public string Email { get; set; } = string.Empty;
//    public string Password { get; set; } = string.Empty;
//}
