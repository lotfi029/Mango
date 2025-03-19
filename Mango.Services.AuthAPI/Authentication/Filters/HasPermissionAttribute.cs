using Microsoft.AspNetCore.Authorization;

namespace Mango.Services.AuthAPI.Authentication.Filters;

public class HasPermissionAttribute(string permission) : AuthorizeAttribute(permission)
{

}
