namespace Mango.Web.Services.IServices;

public interface ITokenProvider
{
    public void SetToken(string token);
    public void RemoveToken();
    public string? GetToken();
}
