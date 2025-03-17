namespace Mango.Web.Abstracts;

public record Request(
    string Url, 
    string AccessToken, 
    ApiType ApiType = ApiType.GET,
    object Data = null!
    );

