using Mango.Web.Abstracts;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services;

public class TokenProvider(IHttpContextAccessor _contextAccessor) : ITokenProvider
{
    public string? GetToken()
    {
        string? token = null;

        _contextAccessor.HttpContext?.Request.Cookies.TryGetValue(ApiSettings.TokenCookie, out token);

        return token;
    }
    public void RemoveToken() => 
        _contextAccessor.HttpContext?.Response.Cookies.Delete(ApiSettings.TokenCookie);
    
    public void SetToken(string token)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddDays(7),
            SameSite = SameSiteMode.None,
            Secure = true
        };
        
        _contextAccessor.HttpContext?.Response.Cookies.Append(ApiSettings.TokenCookie, token, cookieOptions);
    }
}
