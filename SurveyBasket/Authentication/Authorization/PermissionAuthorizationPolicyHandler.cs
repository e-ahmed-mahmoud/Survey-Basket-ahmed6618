using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SurveyBasket.Abstractions.Const;

namespace SurveyBasket.Authentication.Authorization;

public class PermissionAuthorizationPolicyHandler : AuthorizationHandler<PermissionsRequirement>
{
    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionsRequirement requirement)
    {
        var user = context.User.Identity;
        if (user is null || !user.IsAuthenticated)
            return;
        var hasPermission = context.User.Claims.Any(c => c.Value == requirement.Policy && c.Type == PermissionsClaims.Type);
        if (!hasPermission)
            return;
        //check in single line
        // if(context.User.Identity is not { IsAuthenticated : true } userVar 
        //     || !context.User.Claims.Any(x => x.Value == requirement.Policy && x.Type == PermissionsClaims.Type))
        //     return ;

        context.Succeed(requirement);
        return;
    }
}
