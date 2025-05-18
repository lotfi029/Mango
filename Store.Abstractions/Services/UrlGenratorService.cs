//using Microsoft.AspNetCore.Http;

//namespace Store.Abstractions.Services;
//public class UrlGenratorService(IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator) : IUrlGenratorService
//{
//    public string? GetImageUrl(string fileName, string actionName, string controllerName)
//    {
//        if (string.IsNullOrEmpty(fileName))
//            return null!;

//        var httpContext = httpContextAccessor.HttpContext!;

//        return linkGenerator.GetUriByAction(
//            httpContext,
//            action: "GetImage",
//            controller: "Files",
//            values: new { imageName = fileName},
//            scheme: httpContext.Request.Scheme,
//            host: httpContext.Request.Host
//            );
//    }
//}
