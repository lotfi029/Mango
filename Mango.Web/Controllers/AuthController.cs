using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Mango.Web.Controllers;

[Route("auth")]
public class AuthController(
    IAuthService _authService,
    ITokenProvider _tokenProvider) : Controller
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        var request = new LoginRequest("", "");

        return View(request);
    }

    [HttpPost("login")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var result = await _authService.LoginAsync(request);

        if (!result.IsSucceed)
        {
            ModelState.AddModelError(result.Error.Code ?? "Error", result.Error.Description ?? "An error occurred");

            return View(request);
        }
        await SignInAsync(result.Value); 
        _tokenProvider.SetToken(result.Value.AccessToken);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();

        _tokenProvider.RemoveToken();

        return RedirectToAction("login");
    }
    [HttpGet("forbidden")]
    public IActionResult Forbidden()
    {
        return View();
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        var request = new RegisterRequest("", "", "", "", "");

        return View(request);
    }

    [HttpPost("register")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var result = await _authService.RegisterAsync(request);

        if (!result.IsSucceed)
        {
            ModelState.AddModelError(result.Error.Code ?? "Error", result.Error.Description ?? "An error occurred");
            //TempData["error"] = result.Error.Description ?? "An error occurred";
            return View(request);
        }

        var confirmEmailRequest = new ConfirmEmailRequest(result.Value.UserId, result.Value.Code);

        var confirmEmailResult = await _authService.ConfirmEmailAsync(confirmEmailRequest);

        if (!confirmEmailResult.IsSucceed)
        {
            ModelState.AddModelError(string.Empty, confirmEmailResult.Error.Description ?? "An error occurred");
            return View(request);
        }


        return RedirectToAction("Login");
    }

    [HttpGet("forgot-password")]
    public IActionResult ForgotPassword()
    {
        var request = new ForgotPasswordRequest("");

        return View(request);
    }

    [HttpPost("forgot-password")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var result = await _authService.SendResetPasswordCodeAsync(request);

        if (!result.IsSucceed)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "An error occurred");
            return View(request);
        }

        return RedirectToAction("ResetPassword", result.Value);
    }
    [HttpGet("reset-password")]
    public IActionResult ResetPassword(ResetPasswordResponse request) 
        => View(new ResetPasswordRequest(request.Email, request.Code, "", ""));
    
    [HttpPost("reset-password")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
    {
        if (!ModelState.IsValid)
        {
            return View(request);
        }

        var result = await _authService.ResetPasswordAsync(request);

        if (!result.IsSucceed)
        {
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "An error occurred");
            return View(request);
        }

        return RedirectToAction("login");
    }

    private async Task SignInAsync(AuthResponse authResponse)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwt = handler.ReadJwtToken(authResponse.AccessToken);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

        var email = jwt.Claims.FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Email)?.Value;
        var firstName = jwt.Claims.FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.GivenName)?.Value;
        var lastName = jwt.Claims.FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.FamilyName)?.Value;

        if (email == null || firstName == null || lastName == null)
        {
            throw new InvalidOperationException("Required user information is missing in the token.");
        }

        identity.AddClaim(new(JwtRegisteredClaimNames.Sub, jwt.Claims.FirstOrDefault(e => e.Type == JwtRegisteredClaimNames.Sub)!.Value));
        identity.AddClaim(new(JwtRegisteredClaimNames.Email, email));
        identity.AddClaim(new(JwtRegisteredClaimNames.GivenName, firstName));
        identity.AddClaim(new(JwtRegisteredClaimNames.FamilyName, lastName));
        identity.AddClaim(new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

        identity.AddClaim(new(ClaimTypes.Name, firstName + " " + lastName));
        
        var roles = jwt.Claims.FirstOrDefault(e => e.Type == "roles")?.Value ?? "[]";
        var permissions = jwt.Claims.FirstOrDefault(e => e.Type == "permissions")?.Value ?? "[]";

        identity.AddClaim(new(nameof(roles), roles, JsonClaimValueTypes.JsonArray));
        identity.AddClaim(new(nameof(permissions), permissions, JsonClaimValueTypes.JsonArray));

        var principal = new ClaimsPrincipal(identity);
        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            principal);

    }
}
