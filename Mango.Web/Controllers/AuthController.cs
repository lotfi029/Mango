using Mango.Web.Service.IService;

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
            ModelState.AddModelError(string.Empty, result.Error.Description ?? "An error occurred");
            return View(request);
        }
        _tokenProvider.SetToken(result.Value.AccessToken);

        return RedirectToAction("Index", "Home");
    }

    [HttpGet("logout")]
    public IActionResult Logout()
    {
        _tokenProvider.RemoveToken();
        return RedirectToAction("login");
    }

    [HttpGet("register")]
    public IActionResult Register()
    {
        var request = new RegisterRequest("", "", "", "");

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

        return RedirectToAction("ResetPassword");
    }
    [HttpGet("reset-password")]
    public IActionResult ResetPassword()
    {
        var request = new ResetPasswordRequest("", "", "");

        return View(request);
    }
    [HttpPost("reset-password")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ResetPassword(ForgotPasswordRequest request)
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

        return RedirectToAction("ResetPassword");
    }
}
