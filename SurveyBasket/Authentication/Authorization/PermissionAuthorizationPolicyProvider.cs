using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace SurveyBasket.Authentication.Authorization;

public class PermissionAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : DefaultAuthorizationPolicyProvider(options)
{
    private readonly AuthorizationOptions _authorizationOptions = options.Value;
    public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        var policy = await base.GetPolicyAsync(policyName);
        if (policy is not null)     //if there is a defined policy
            return policy;

        // build policy if not defined
        var permissionPolicy = new AuthorizationPolicyBuilder()
            .AddRequirements(new PermissionsRequirement(policyName)).Build();

        _authorizationOptions.AddPolicy(policyName, permissionPolicy);
        return permissionPolicy;
    }
}
