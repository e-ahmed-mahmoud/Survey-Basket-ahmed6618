using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SurveyBasket.Authentication.Authorization;
//pass policyName to Base class
public class HasPermissionAttribute(string policyName) : AuthorizeAttribute(policyName);
