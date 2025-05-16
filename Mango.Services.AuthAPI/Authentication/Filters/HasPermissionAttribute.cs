using Microsoft.AspNetCore.Authorization;

namespace Store.Services.AuthAPI.Authentication.Filters;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{

}
