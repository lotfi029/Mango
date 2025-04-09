namespace Mango.Web.Service.IService;

public interface ITokenProvider
{
    public void SetToken(string token);
    public void RemoveToken();
    public string? GetToken();
}
