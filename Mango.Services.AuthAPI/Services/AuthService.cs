using Store.Services.AuthAPI.Abstracts;
using Store.Services.AuthAPI.Abstracts.Constants;
using Store.Services.AuthAPI.Authentication;
using Store.Services.AuthAPI.Entities;
using Store.Services.AuthAPI.Errors;
using Store.Services.AuthAPI.Options;
using Store.Services.AuthAPI.Persistence;
using Mapster;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Store.Services.AuthAPI.Contracts.Auths;
using System.Security.Cryptography;
using System.Text;
using Store.Abstractions.Abstraction;

namespace Store.Services.AuthAPI.Services;

public class AuthService(
    UserManager<AppUser> _userManager,
    IJwtProvider _jwtProvider,
    SignInManager<AppUser> _signInManager,
    IEmailSender _emailSender,
    IHttpContextAccessor _contextAccessor,
    IOptions<MailOptions> mailSettings,
    AppDbContext _context) : IAuthService
{

    private readonly MailOptions _mailSettings = mailSettings.Value;
    private readonly int _refreshTokenExpiryDays = 14;

    public async Task<Result<AccessTokenResponse>> GetTokenAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return AuthErrors.InvalidCredentials;

        if (user.IsDisable)
            return AuthErrors.DisabledUser;

        var result = await _signInManager.PasswordSignInAsync(user, request.Password, false, true);

        if (!result.Succeeded)
        {

            return result.IsNotAllowed
                ? AuthErrors.EmailIsNotConfirmed
                : result.IsLockedOut
                ? AuthErrors.LockedUser
                : AuthErrors.InvalidCredentials;
        }
        var (roles, permission) = await GetUserRolesAndClaims(user, cancellationToken);
        var (token, expiresIn) = _jwtProvider.GenerateToken(user, roles, permission);

        var refreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = refreshToken,
            ExpiresOn = refreshTokenExpiration,
        });

        await _userManager.UpdateAsync(user);

        var respone = new AccessTokenResponse()
        {
            ExpiresIn = expiresIn,
            AccessToken = token,
            RefreshToken = refreshToken
        };

        return Result.Success(respone);
    }

    public async Task<Result<AccessTokenResponse>> GetRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(request.Token);

        if (userId is null)
            return RefreshTokenErrors.InvalidToken;

        if (await _userManager.FindByIdAsync(userId) is not { } user)
            return RefreshTokenErrors.InvalidUserId;

        if (user.IsDisable)
            return AuthErrors.DisabledUser;

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(e => e.Token == request.RefreshToken && e.IsActive);

        if (userRefreshToken is null)
            return RefreshTokenErrors.NoRefreshToken;

        userRefreshToken.RevokeOn = DateTime.UtcNow;

        var (roles, permission) = await GetUserRolesAndClaims(user, cancellationToken);
        var (newToken, expiresIn) = _jwtProvider.GenerateToken(user, roles, permission);

        var newRefreshToken = GenerateRefreshToken();
        var refreshTokenExpiration = DateTime.UtcNow.AddDays(_refreshTokenExpiryDays);

        user.RefreshTokens.Add(new RefreshToken
        {
            Token = newRefreshToken,
            ExpiresOn = refreshTokenExpiration,
        });

        await _userManager.UpdateAsync(user);

        var respone = new AccessTokenResponse()
        {
            ExpiresIn = expiresIn,
            AccessToken = request.Token,
            RefreshToken = request.RefreshToken
        };

        return Result.Success(respone);
    }
    public async Task<Result> RevokeRefreshTokenAsync(RefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        var userId = _jwtProvider.ValidateToken(request.Token);

        if (userId is null)
            return RefreshTokenErrors.InvalidToken;

        var user = await _userManager.FindByIdAsync(userId);

        if (user is null)
            return RefreshTokenErrors.InvalidUserId;

        var userRefreshToken = user.RefreshTokens.SingleOrDefault(e => e.Token == request.RefreshToken && e.IsActive);

        if (userRefreshToken is null)
            return RefreshTokenErrors.NoRefreshToken;

        userRefreshToken.RevokeOn = DateTime.UtcNow;

        await _userManager.UpdateAsync(user);

        return Result.Success();
    }
    public async Task<Result<ConfirmEmailRequest>> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        if (await _userManager.Users.AnyAsync(e => e.Email == request.Email, cancellationToken))
            return AuthErrors.DuplicatedEmail;


        var user = request.Adapt<AppUser>();
        var createUserResult = await _userManager.CreateAsync(user, request.Password);

        if (!createUserResult.Succeeded)
        {
            var error = createUserResult.Errors.First();
            return Error.BadRequest(error.Description);
        }

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        //await SendEmailAsync(user, code);
        //_logger.LogInformation("Confirmation Email Code: {code} {mailSettings}", code, _mailSettings.ToString());

        return Result.Success(new ConfirmEmailRequest(user.Id, code));
    }
    private async Task SendEmailAsync(AppUser user, string code)
    {
        var origin = _contextAccessor.HttpContext?.Request.Headers.Origin;

        var emailBody = EmailBodyBuilder.GenerateEmailBody("EmailConfirmation",
            new Dictionary<string, string>()
            {
                {"{{name}}",user.FirstName},
                {"{{action_url}}", $"{origin}/auth/confirm?userId={user.Id}&code={code}"}
            }
        );

        //BackgroundJob.Enqueue(() => );
        await _emailSender.SendEmailAsync(user.Email!, " Mango: Email Confirmation", emailBody);

        await Task.CompletedTask;
    }

    public async Task<Result> ConfirmEmailAsync(ConfirmEmailRequest request)
    {

        if (await _userManager.FindByIdAsync(request.UserId) is not { } user)
            return AuthErrors.InvalidCode;

        if (user.EmailConfirmed)
            return AuthErrors.DuplicatedConfirmation;

        var code = request.Code;
        try
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
        }
        catch (FormatException)
        {
            return AuthErrors.InvalidCode;
        }

        var result = await _userManager.ConfirmEmailAsync(user, code);

        if (!result.Succeeded)
        {
            var error = result.Errors.FirstOrDefault()!;
            return Error.BadRequest(error.Description);
        }

        await _userManager.AddToRoleAsync(user, DefaultRoles.UserName);
        return Result.Success();
    }
    public async Task<Result<ConfirmEmailRequest>> ReConfirmAsync(ResendConfirmationEmailRequest request)
    {

        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return AuthErrors.NotFound;

        if (user.EmailConfirmed)
            return AuthErrors.DuplicatedConfirmation;

        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        //await SendEmailAsync(user, code);

        //_logger.LogInformation("ReConfirmation Email: {code}", code);

        return Result.Success(new ConfirmEmailRequest(user.Id, code));
    }

    public async Task<Result<ResetPasswordCodeResponse>> SendResetPasswordCodeAsync(ForgotPasswordRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return Result.Success(new ResetPasswordCodeResponse("", ""));

        if (!user.EmailConfirmed)
            return AuthErrors.EmailIsNotConfirmed;

        var code = await _userManager.GeneratePasswordResetTokenAsync(user);

        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

        //_logger.LogInformation("Confirmation Code: {code}", code);

        var response = new ResetPasswordCodeResponse(user.Email!, code);

        return Result.Success(response);
    }
    public async Task<Result> ResetPasswordAsync(ResetPasswordRequest request)
    {
        if (await _userManager.FindByEmailAsync(request.Email) is not { } user)
            return AuthErrors.InvalidCode;

        var code = request.ResetCode;

        IdentityResult result;
        try
        {
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            if (await _userManager.CheckPasswordAsync(user, request.NewPassword))
                return Error.Conflict("this password is used before Select another one.");
            result = await _userManager.ResetPasswordAsync(user, code, request.NewPassword);
        }
        catch (FormatException)
        {
            result = IdentityResult.Failed(_userManager.ErrorDescriber.InvalidToken());
        }

        if (!result.Succeeded)
        {
            var error = result.Errors.FirstOrDefault()!;
            return Error.BadRequest(error.Description);
        }

        return Result.Success();
    }
    private static string GenerateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }

    private async Task<(IEnumerable<string> roles, IEnumerable<string> permissions)> GetUserRolesAndClaims(AppUser user, CancellationToken cancellationToken = default)
    {
        var roles = await _userManager.GetRolesAsync(user);

        var permission = await (
            from r in _context.Roles
            join c in _context.RoleClaims
            on r.Id equals c.RoleId
            where roles.Contains(r.Name!)
            select c.ClaimValue)
            .Distinct()
            .ToListAsync(cancellationToken);

        return (roles, permission);
    }
}