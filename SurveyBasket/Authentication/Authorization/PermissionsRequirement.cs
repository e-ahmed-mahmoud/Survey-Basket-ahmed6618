using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Authentication.Authorization;

public class PermissionsRequirement(string policy) : IAuthorizationRequirement
{
    public string Policy { get; } = policy;
}
