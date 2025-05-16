﻿using Store.Services.AuthAPI.Abstracts.Constants;
using Microsoft.AspNetCore.Authorization;

namespace Store.Services.AuthAPI.Authentication.Filters;

public class PermissionAutherizationHandler : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
    {
        if (context.User.Identity is not { IsAuthenticated: true } ||
            !context.User.Claims.Any(e => e.Value == requirement.Permission && e.Type == Permissions.Type))
            return;

        context.Succeed(requirement);

        return;
    }
}
